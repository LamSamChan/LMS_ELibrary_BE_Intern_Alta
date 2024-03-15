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
using LMS_Library_API.Services.ServiceAboutExam.QuestionBankService;
using LMS_Library_API.Services.ServiceAboutSubject.LessonService;
using LMS_Library_API.ViewModels;
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
        private readonly IQuestionBankSvc _questionBankSvc;
        private readonly IMapper _mapper;
        private readonly IExportFileExamSvc _exportFileExamSvc;
        private readonly IBlobStorageSvc _blobStorageSvc;

        public ExamsController(IExamSvc examSvc, IQuestionBankSvc questionBankSvc, IMapper mapper, IExportFileExamSvc exportFileExamSvc, IBlobStorageSvc blobStorageSvc)
        {
            _mapper = mapper;
            _examSvc = examSvc;
            _questionBankSvc = questionBankSvc;
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

        [HttpPost("{filePath}")]
        public async Task<ActionResult<Logger>> UploadExam(string filePath)
        {
            var loggerResult = await _examSvc.CreateMCExamByFile(filePath);

            if (loggerResult.status == TaskStatus.RanToCompletion)
            {
                BlobContentModel? uploadModel = loggerResult.data as BlobContentModel;

                var uploadResult = await _blobStorageSvc.UploadBlobFile(uploadModel);

                if (uploadResult.status == TaskStatus.Faulted)
                {
                    return BadRequest(uploadResult);
                }

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
            if (!examDTO.Format)
            {
                return BadRequest(new Logger()
                {
                    status= TaskStatus.Faulted,
                    message = "Sai định dạng đề thi / kiểm tra!"
                });
            }

            var exam = _mapper.Map<Exam>(examDTO);

            var loggerResult = await CreateAndExportExam(exam);

            if (loggerResult.status == TaskStatus.RanToCompletion)
            {
                return Ok(loggerResult);
            }
            else
            {
                return BadRequest(loggerResult);
            }
        }

        [HttpPost("create/essay-exam")]
        public async Task<ActionResult<Logger>> CreateEssayExam(Essay_ExamDTO examDTO)
        {
            if (examDTO.Format)
            {
                return BadRequest(new Logger()
                {
                    status = TaskStatus.Faulted,
                    message = "Sai định dạng đề thi / kiểm tra!"
                });
            }

            var exam = _mapper.Map<Exam>(examDTO);

            var loggerResult = await CreateAndExportExam(exam);

            if (loggerResult.status == TaskStatus.RanToCompletion)
            {
                return Ok(loggerResult);
            }
            else
            {
                return BadRequest(loggerResult);
            }
        }

        [HttpPost("auto-create-mc-exam")]
        public async Task<ActionResult<Logger>> AutoCreateExam(AutoCreateMCExam autoCreateExam)
        {
            if (autoCreateExam.QuestionsEasy + autoCreateExam.QuestionsMedium + autoCreateExam.QuestionsHard != autoCreateExam.NumberOfQuestions)
            {
                Logger logger = new Logger() { 
                    status = TaskStatus.Faulted,
                    message = "Số lượng tổng câu hỏi và tổng số lượng câu hỏi của 3 mức độ không khớp"
                };
                return BadRequest(logger); 
            }

            for (int i = 0; i < autoCreateExam.NumberOfExams; i++)
            {
                var exam = _mapper.Map<Exam>(autoCreateExam.AutoExamDTO);
                exam.Id = Guid.NewGuid().ToString().Substring(0, 4) + DateTime.Now.Ticks.ToString().Substring(7, 4);
                exam.FileType = ".xlsx";
                exam.Format = true;
                exam.Duration = 60;
                exam.Status = Enums.Status.PendingApproval;
                exam.Question_Exam = await _questionBankSvc.GetRandomQuestion(autoCreateExam.QuestionsEasy, autoCreateExam.QuestionsMedium, autoCreateExam.QuestionsHard, exam.SubjectId.ToUpper());

                var loggerResult = await CreateAndExportExam(exam);

                if (loggerResult.status == TaskStatus.Faulted)
                {
                    return BadRequest(loggerResult);
                }
            }
 
            return Ok(new Logger()
            {
                status = TaskStatus.RanToCompletion,
                message = "Thành công"
            });
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

        [HttpGet("{id}/{format}")]
        public async Task<ActionResult<Logger>> GetDetailExam(string id, bool format)
        {
            if (!String.IsNullOrWhiteSpace(id))
            {
                var loggerResult = await _examSvc.GetDetailExam(id, format);
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

        [HttpPut("update/mc-exam")]
        public async Task<ActionResult<Logger>> UpdateMCExam(MC_ExamDTO examDTO)
        {
            var exam = _mapper.Map<Exam>(examDTO);

            var getOldExam = await _examSvc.GetById(examDTO.Id);

            if (getOldExam.status == TaskStatus.Faulted)
            {
                return BadRequest(getOldExam);
            }

            var oldPath = (Exam)getOldExam.data;

            var exportExam = await _exportFileExamSvc.ExportExamToExcel(exam);

            var uploadResult = await _blobStorageSvc.UploadBlobFile(exportExam);

            if (uploadResult.status == TaskStatus.Faulted)
            {
                return BadRequest(uploadResult);
            }

            exam.FilePath = (string)uploadResult.data;
            exam.FileType = Path.GetExtension(exportExam.FileName);

            var loggerResult = await _examSvc.Update(exam);

            if (loggerResult.status == TaskStatus.Faulted)
            {
                return BadRequest(loggerResult);
            }

            var deleteResult = await _blobStorageSvc.DeleteBlobFile(oldPath.FilePath, "document");

            if (deleteResult.status == TaskStatus.Faulted)
            {
                return BadRequest(deleteResult);
            }

            return Ok(loggerResult);
        }

        [HttpPut("update/essay-exam")]
        public async Task<ActionResult<Logger>> UpdateEssayExam(Essay_ExamDTO examDTO)
        {
            var exam = _mapper.Map<Exam>(examDTO);

            var getOldExam = await _examSvc.GetById(examDTO.Id);

            if (getOldExam.status == TaskStatus.Faulted)
            {
                return BadRequest(getOldExam);
            }

            var oldPath = (Exam)getOldExam.data;

            var exportExam = await _exportFileExamSvc.ExportExamToWord(exam);

            var uploadResult = await _blobStorageSvc.UploadBlobFile(exportExam);

            if (uploadResult.status == TaskStatus.Faulted)
            {
                return BadRequest(uploadResult);
            }

            exam.FilePath = (string)uploadResult.data;
            exam.FileType = Path.GetExtension(exportExam.FileName);

            var loggerResult = await _examSvc.Update(exam);

            if (loggerResult.status == TaskStatus.Faulted)
            {
                return BadRequest(loggerResult);
            }

            var deleteResult = await _blobStorageSvc.DeleteBlobFile(oldPath.FilePath, "document");

            if (deleteResult.status == TaskStatus.Faulted)
            {
                return BadRequest(deleteResult);
            }

            return Ok(loggerResult);
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

        private async Task<Logger> CreateAndExportExam(Exam exam)
        {
            var loggerResult = await _examSvc.Create(exam);

            if (loggerResult.status == TaskStatus.RanToCompletion)
            {
                BlobContentModel exportFile;
                if (exam.Format)
                {
                    exportFile = await _exportFileExamSvc.ExportExamToExcel(exam);
                }
                else
                {
                    exportFile = await _exportFileExamSvc.ExportExamToWord(exam);
                }

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

                            return updateResult;
                        }
                        else
                        {
                            return updateResult;
                        }
                    }
                    else
                    {
                        return getExam;
                    }
                }
                else
                {
                    return uploadResult;
                }
            }
            else
            {
                return loggerResult;
            }
        }
    }
}
