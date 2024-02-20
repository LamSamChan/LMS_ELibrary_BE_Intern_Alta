using AutoMapper;
using LMS_Library_API.Helpers.BlobHelperService;
using LMS_Library_API.Models;
using LMS_Library_API.Models.AboutUser;
using LMS_Library_API.ModelsDTO;
using LMS_Library_API.Services.ServiceAboutUser.PrivateFileService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Update.Internal;

namespace LMS_Library_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrivateFilesController : ControllerBase
    {
        private readonly IPrivateFileSvc _privateFileSvc;
        private readonly IBlobStorageSvc _blobStorageSvc;
        private readonly IMapper _mapper;

        public PrivateFilesController(IPrivateFileSvc privateFileSvc, IBlobStorageSvc blobStorageSvc, IMapper mapper)
        {
            _privateFileSvc = privateFileSvc;
            _blobStorageSvc = blobStorageSvc;
            _mapper = mapper;
        }

        [HttpPost("UploadFile")]
        public async Task<ActionResult<Logger>> UploadFile(PrivateFileDTO privateFileDTO)
        {
            var newFile = _mapper.Map<PrivateFile>(privateFileDTO);
            var uploadResult = await _blobStorageSvc.UploadBlobFile(privateFileDTO.BlobContent);
            if (uploadResult.status == TaskStatus.RanToCompletion)
            {
                newFile.Name = privateFileDTO.BlobContent.FileName;
                newFile.FilePath = uploadResult.data.ToString();
                newFile.IsImage = privateFileDTO.BlobContent.isImage;
            }
            else
            {
                return BadRequest(uploadResult);
            }

            var loggerResult = await _privateFileSvc.Create(newFile);
            if (loggerResult.status == TaskStatus.RanToCompletion)
            {
                return Ok(loggerResult);
            }
            else
            {
                var deleteResult = await _blobStorageSvc.DeleteBlobFile(uploadResult.data.ToString(), privateFileDTO.BlobContent.isImage ? "image" : "document");
                return BadRequest(loggerResult);
            }
        }

        [HttpGet]
        public async Task<ActionResult<Logger>> GetAll()
        {
            var loggerResult = await _privateFileSvc.GetAll();
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
                var loggerResult = await _privateFileSvc.GetById(id);
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

        [HttpGet("user/{id}")]
        public async Task<ActionResult<Logger>> GetByUserId(string id)
        {
            if (!String.IsNullOrWhiteSpace(id))
            {
                var loggerResult = await _privateFileSvc.GetByUserId(id);
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

        [HttpGet("search/{userId}/{query}")]
        public async Task<ActionResult<Logger>> Search(string userId, string query)
        {
            if (!String.IsNullOrWhiteSpace(query))
            {
                var loggerResult = await _privateFileSvc.Search(userId, query);
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
                return BadRequest("Hãy điền nội dung để tìm kiếm đối tượng");
            }
        }

        [HttpPut]
        public async Task<ActionResult<Logger>> Update(PUTPrivateFileDTO privateFileDTO)
        {
            var privateFile = _mapper.Map<PrivateFile>(privateFileDTO);

            var getOldFile = await _privateFileSvc.GetById(privateFile.Id);

            var oldFile = (PrivateFile)getOldFile.data;

            string fileName = oldFile.Name;

            var updateFile = await _blobStorageSvc.ChangeFileName(fileName, privateFile.Name);

            if (updateFile.status == TaskStatus.Faulted)
            {
                return updateFile;
            }

            privateFile.FilePath = updateFile.data.ToString();
            var loggerResult = await _privateFileSvc.Update(privateFile);
            if (loggerResult.status == TaskStatus.RanToCompletion)
            {
                return Ok(loggerResult);
            }
            else
            {
                return BadRequest(loggerResult);
            }
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<Logger>> Delete(int id)
        {
            if (!String.IsNullOrWhiteSpace(id.ToString()))
            {
                var loggerResult = await _privateFileSvc.Delete(id);

                var file = (PrivateFile)loggerResult.data;

                if (loggerResult.status == TaskStatus.RanToCompletion)
                {
                    var deleleBlobResult = await _blobStorageSvc.DeleteBlobFile(file.FilePath, file.IsImage ? "image" : "document");

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
