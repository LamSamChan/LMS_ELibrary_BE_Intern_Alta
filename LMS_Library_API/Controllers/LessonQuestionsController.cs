using AutoMapper;
using LMS_Library_API.Models;
using LMS_Library_API.Models.AboutSubject;
using LMS_Library_API.ModelsDTO;
using LMS_Library_API.Services.ServiceAboutSubject.LessonQuestionService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LMS_Library_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LessonQuestionsController : ControllerBase
    {
        private readonly ILessonQuestionSvc _lessonQuesstionSvc;
        private readonly IMapper _mapper;

        public LessonQuestionsController(ILessonQuestionSvc lessonQuestionSvc, IMapper mapper)
        {
            _lessonQuesstionSvc = lessonQuestionSvc;
            _mapper = mapper;
        }
        /// <summary>
        /// để studentId = null
        /// </summary>
        [HttpPost("teacher")]
        public async Task<ActionResult<Logger>> TeacherCreate(LessonQuestionDTO lessonQuestionDTO)
        {
            var lessonQuestion = _mapper.Map<LessonQuestion>(lessonQuestionDTO);

            lessonQuestion.isTeacher = true;

            var loggerResult = await _lessonQuesstionSvc.Create(lessonQuestion);
            if (loggerResult.status == TaskStatus.RanToCompletion)
            {
                return Ok(loggerResult);
            }
            else
            {
                return BadRequest(loggerResult);
            }
        }

        /// <summary>
        /// để teacherId = null
        /// </summary>
        [HttpPost("student")]
        public async Task<ActionResult<Logger>> StudentCreate(LessonQuestionDTO lessonQuestionDTO)
        {
            var lessonQuestion = _mapper.Map<LessonQuestion>(lessonQuestionDTO);

            lessonQuestion.isTeacher = false;

            var loggerResult = await _lessonQuesstionSvc.Create(lessonQuestion);
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
            var loggerResult = await _lessonQuesstionSvc.GetAll();
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
                var loggerResult = await _lessonQuesstionSvc.GetById(id);
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

        [HttpGet("lesson/{id}")]
        public async Task<ActionResult<Logger>> GetByLessonId(int id)
        {
            if (!String.IsNullOrWhiteSpace(id.ToString()))
            {
                var loggerResult = await _lessonQuesstionSvc.GetByLessonId(id);
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
        public async Task<ActionResult<Logger>> Update(LessonQuestionDTO lessonQuestionDTO)
        {
            var lessonQuestion = _mapper.Map<LessonQuestion>(lessonQuestionDTO);

            var loggerResult = await _lessonQuesstionSvc.Update(lessonQuestion);
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
                var loggerResult = await _lessonQuesstionSvc.Delete(id);
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
    }
}
