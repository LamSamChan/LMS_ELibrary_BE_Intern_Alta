﻿using AutoMapper;
using LMS_Library_API.Helpers.BlobHelperService;
using LMS_Library_API.Models;
using LMS_Library_API.Models.AboutSubject;
using LMS_Library_API.Models.AboutUser;
using LMS_Library_API.Models.BlobStorage;
using LMS_Library_API.ModelsDTO;
using LMS_Library_API.Services.ServiceAboutSubject.DocumentAccessService;
using LMS_Library_API.Services.ServiceAboutSubject.DocumentService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace LMS_Library_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class DocumentsController : ControllerBase
    {
        private readonly IDocumentSvc _documentSvc;
        private readonly IDocumentAccessSvc _documentAccessSvc;
        private readonly IBlobStorageSvc _blobStorageSvc;
        private readonly IMapper _mapper;

        public DocumentsController(IDocumentSvc documentSvc, IDocumentAccessSvc documentAccessSvc, IBlobStorageSvc blobStorageSvc, IMapper mapper)
        {
            _documentSvc = documentSvc;
            _documentAccessSvc = documentAccessSvc;
            _blobStorageSvc = blobStorageSvc;
            _mapper = mapper;
        }
        /// <summary>
        /// Type => false: tài liệu đi kèm của bài giảng, true: tài liệu hiển thị cho bài giảng 
        /// Status: 0 -> Chưa gửi phê duyệt | 1: -> Đang chờ phê duyệt | 2: Đã phê duyệt | 3: Đã từ chối duyệt | 4: Đã huỷ phê duyệt"
        /// </summary>

        [HttpPost]
        public async Task<ActionResult<Logger>> Create(DocumentDTO documentDTO)
        {
            var document = _mapper.Map<Document>(documentDTO);

            if (documentDTO.FilePath.FilePath != null && documentDTO.FilePath.FileName != null)
            {

                var filePath = await _blobStorageSvc.UploadBlobFile(documentDTO.FilePath);

                if (filePath.status == TaskStatus.RanToCompletion)
                {
                    document.FilePath = filePath.data.ToString();
                }
                else
                {
                    return BadRequest(filePath);
                }
            }
            else
            {
                return BadRequest( new Logger()
                {
                    status = TaskStatus.Faulted,
                    message = "Hãy tải tệp lên"
                });
            }

            var loggerResult = await _documentSvc.Create(document);
            if (loggerResult.status == TaskStatus.RanToCompletion)
            {
                var documentAccess = _mapper.Map<DocumentAccess>(documentDTO.DocumentAccess);

                if ((documentAccess.classId != null && documentAccess.isForAllClasses) || (documentAccess.classId == null && !documentAccess.isForAllClasses))
                {
                    return BadRequest(new Logger()
                    {
                        status = TaskStatus.Faulted,
                        message = "Hãy kiểm tra lại phân công bài học"
                    });
                }

                var addDocumentAccess = await _documentAccessSvc.Create(documentAccess);

                if (addDocumentAccess.status == TaskStatus.Faulted)
                {
                    return BadRequest(addDocumentAccess);
                }

                return Ok(loggerResult);
            }
            else
            {
                return BadRequest(loggerResult);
            }
        }

        [HttpGet]
        public async Task<ActionResult<Logger>> GetAll()
        {
            var loggerResult = await _documentSvc.GetAll();
            if (loggerResult.status == TaskStatus.RanToCompletion)
            {
                return Ok(loggerResult);
            }
            else
            {
                return BadRequest(loggerResult);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Logger>> GetById(int id)
        {
            if (!String.IsNullOrWhiteSpace(id.ToString()))
            {
                var loggerResult = await _documentSvc.GetById(id);
                if (loggerResult.status == TaskStatus.RanToCompletion)
                {
                    return Ok(loggerResult);
                }
                else
                {
                    return BadRequest(loggerResult);
                }
            }
            else
            {
                return BadRequest("Hãy điền ID để tìm kiếm đối tượng");
            }
        }

        [HttpPut]
        public async Task<ActionResult<Logger>> Update(DocumentDTO documentDTO)
        {
            var document = _mapper.Map<Document>(documentDTO);

            if (documentDTO.FilePath.FilePath != null && documentDTO.FilePath.FileName != null)
            {
                var filePath = await _blobStorageSvc.UploadBlobFile(documentDTO.FilePath);

                if (filePath.status == TaskStatus.RanToCompletion)
                {
                    document.FilePath = filePath.data.ToString();

                    var oldDocData = (Document)_documentSvc.GetById(documentDTO.Id).Result.data;
                    var deleteOldDocFile = _blobStorageSvc.DeleteBlobFile(oldDocData.FilePath, "document");

                    if (deleteOldDocFile.IsFaulted)
                    {
                        return BadRequest(deleteOldDocFile);
                    }
                }
                else
                {
                    return BadRequest(filePath);
                }
            }
            else
            {
                document.FilePath = null;
            }

            var loggerResult = await _documentSvc.Update(document);
            if (loggerResult.status == TaskStatus.RanToCompletion)
            {
                return Ok(loggerResult);
            }
            else
            {
                return BadRequest(loggerResult);
            }
        }

        [HttpGet("DownloadFile")]
        public async Task<ActionResult<Logger>> DownloadFile(int fileId, string containerName)
        {
            var getFile = await _documentSvc.GetById(fileId);
            if (getFile.status == TaskStatus.Faulted)
            {
                return BadRequest(getFile);
            }
            var document = (Document)getFile.data;

            BlobObject blobObject = await _blobStorageSvc.GetBlobFile(document.FilePath, containerName);

            if (blobObject == null)
            {
                return BadRequest(new Logger
                {
                    status = TaskStatus.Faulted,
                    message = "Container hoặc File không tồn tại, hãy kiểm tra lại"
                });
            }
            if (blobObject.ContentType.Contains("image"))
            {
                return File(blobObject.Content, blobObject.ContentType);
            }
            else
            {
                return File(blobObject.Content, blobObject.ContentType, document.Name);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Logger>> Delete(int id)
        {
           if (!String.IsNullOrWhiteSpace(id.ToString()))
           {
                    var loggerResult = await _documentSvc.Delete(id);

                    var file = (Document)loggerResult.data;

                    if (loggerResult.status == TaskStatus.RanToCompletion)
                    {
                        var deleleBlobResult = await _blobStorageSvc.DeleteBlobFile(file.FilePath,"document");

                        if (deleleBlobResult.status == TaskStatus.RanToCompletion)
                        {
                            return Ok(loggerResult);
                        }
                        else
                        {
                            return Ok(deleleBlobResult);

                        }
                    }
                    else
                    {
                        return BadRequest(loggerResult);
                    }
           }
           else
           {
                    return BadRequest("Hãy điền ID để xoá đối tượng");
          }                        
            
        }

    }
}
