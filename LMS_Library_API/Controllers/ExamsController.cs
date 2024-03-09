using AutoMapper;
using LMS_Library_API.Models;
using LMS_Library_API.Models.AboutSubject;
using LMS_Library_API.Models.Exams;
using LMS_Library_API.ModelsDTO;
using LMS_Library_API.Services.ExamService;
using LMS_Library_API.Services.ServiceAboutSubject.LessonService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LMS_Library_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamsController : ControllerBase
    {
        private readonly IExamSvc _examSvc;
        private readonly IMapper _mapper;

        public ExamsController(IExamSvc examSvc, IMapper mapper)
        {
            _mapper = mapper;
            _examSvc = examSvc;
        }

        /// <summary>
        /// Mặc định censorId (người kiểm duyệt) khi vừa tạo sẽ là ID của người tạo, khi có người duyệt sẽ cập nhật Id của người duyệt vào ||
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
