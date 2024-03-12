using AutoMapper;
using LMS_Library_API.Helpers.BlobHelperService;
using LMS_Library_API.Helpers.ExportFileExamService;
using LMS_Library_API.Models;
using LMS_Library_API.Models.AboutSubject;
using LMS_Library_API.Models.AboutUser;
using LMS_Library_API.Models.BlobStorage;
using LMS_Library_API.Models.Exams;
using LMS_Library_API.ModelsDTO;
using LMS_Library_API.Services.ExamService;
using LMS_Library_API.Services.ServiceAboutSubject.LessonService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LMS_Library_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ExamsController : ControllerBase
    {
        private readonly IExamSvc _examSvc;
        private readonly IMapper _mapper;
        private readonly IExportFileExamSvc _exportFileExamSvc;
        private readonly IBlobStorageSvc _blobStorageSvc;

        public ExamsController(IExamSvc examSvc, IMapper mapper, IExportFileExamSvc exportFileExamSvc, IBlobStorageSvc blobStorageSvc)
        {
            _mapper = mapper;
            _examSvc = examSvc;
            _exportFileExamSvc = exportFileExamSvc;
            _blobStorageSvc = blobStorageSvc;
        }

        /// <summary>
        /// Mặc định censorId (người kiểm duyệt) khi vừa tạo sẽ là null
        /// Status: 0 -> Chưa gửi phê duyệt | 1: -> Đang chờ phê duyệt | 2: Đã phê duyệt | 3: Đã từ chối duyệt | 4: Đã huỷ phê duyệt | 5:Lưu nháp" ||
        /// Format: false -> tự luận true -> trắc nghiệm
        /// </summary>

        [HttpPost]
        public async Task<ActionResult<Logger>> Create(ExamDTO examDTO)
        {
            var exam = _mapper.Map<Exam>(examDTO);

            var loggerResult = await _examSvc.Create(exam);
            if (loggerResult.status == TaskStatus.RanToCompletion)
            {
                return Ok(loggerResult);
            }
            else
            {
                return BadRequest(loggerResult);
            }
        }

        [HttpPost("create/mc-exam")]
        public async Task<ActionResult<Logger>> CreateMCExam(MC_ExamDTO examDTO)
        {
            var exam = _mapper.Map<Exam>(examDTO);

            var loggerResult = await _examSvc.Create(exam);

            if (loggerResult.status == TaskStatus.RanToCompletion)
            {

                var exportFile = await _exportFileExamSvc.ExportExamToExcel(exam);

                var uploadResult = await _blobStorageSvc.UploadBlobFile(exportFile);

                if (uploadResult.status == TaskStatus.RanToCompletion)
                {
                    string filePath = (string)uploadResult.data;
                    string fileExtension = Path.GetExtension(exportFile.FileName);

                    var getExam = await _examSvc.GetById(exam.Id);

                    if (getExam.status == TaskStatus.RanToCompletion)
                    {
                        var updateExam = (Exam)getExam.data;

                        updateExam.FileType = fileExtension;
                        updateExam.FilePath = filePath;

                        var updateResult = await _examSvc.Update(updateExam);

                        if (updateResult.status == TaskStatus.RanToCompletion)
                        {
                            System.GC.Collect();
                            System.GC.WaitForPendingFinalizers();
                            System.IO.File.Delete(exportFile.FilePath);

                            return Ok(updateResult);
                        }
                        else
                        {
                            return BadRequest(updateResult);
                        }
                    }
                    else
                    {
                        return BadRequest(getExam);
                    }
                }
                else
                {
                    return BadRequest(uploadResult);
                }
            }
            else
            {
                return BadRequest(loggerResult);
            }
        }

        [HttpPost("create/essay-exam")]
        public async Task<ActionResult<Logger>> CreateEssayExam(Essay_ExamDTO examDTO)
        {
            var exam = _mapper.Map<Exam>(examDTO);

            var loggerResult = await _examSvc.Create(exam);
            if (loggerResult.status == TaskStatus.RanToCompletion)
            {
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
            var loggerResult = await _examSvc.GetAll();
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
        public async Task<ActionResult<Logger>> GetDetailExam(string id)
        {
            if (!String.IsNullOrWhiteSpace(id))
            {
                var loggerResult = await _examSvc.GetDetailExam(id);
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

        [HttpGet("DownloadFile/{id}")]
        public async Task<ActionResult<Logger>> DownloadFile(string id)
        {
            var getFile = await _examSvc.GetById(id.ToUpper());
            if (getFile.status == TaskStatus.Faulted)
            {
                return BadRequest(getFile);
            }
            var examFile = (Exam)getFile.data;

            BlobObject blobObject = await _blobStorageSvc.GetBlobFile(examFile.FilePath, "document");

            if (blobObject == null)
            {
                return BadRequest(new Logger
                {
                    status = TaskStatus.Faulted,
                    message = "Container hoặc File không tồn tại, hãy kiểm tra lại"
                });
            }
            return File(blobObject.Content, blobObject.ContentType, examFile.Id+"-"+examFile.FileName);     
        }

        [HttpPut]
        public async Task<ActionResult<Logger>> Update(ExamDTO examDTO)
        {
            var exam = _mapper.Map<Exam>(examDTO);

            var loggerResult = await _examSvc.Update(exam);
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
        public async Task<ActionResult<Logger>> Delete(string id)
        {
            if (!String.IsNullOrWhiteSpace(id))
            {
                var loggerResult = await _examSvc.Delete(id);
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
                return BadRequest("Hãy điền ID để xoá đối tượng");
            }
        }

        [HttpGet("search/{query}")]
        public async Task<ActionResult<Logger>> Search(string query)
        {
            if (!String.IsNullOrWhiteSpace(query))
            {
                var loggerResult = await _examSvc.Search(query);
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
                return BadRequest("Hãy điền nội dung tìm kiếm");
            }
        }
    }
}
