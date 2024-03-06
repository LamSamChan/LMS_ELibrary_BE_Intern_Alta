using AutoMapper;
using LMS_Library_API.Models;
using LMS_Library_API.Models.AboutSubject;
using LMS_Library_API.ModelsDTO;
using LMS_Library_API.Services.ServiceAboutSubject.LessonAnswerService;
using LMS_Library_API.Services.ServiceAboutSubject.LessonAnswerService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LMS_Library_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LessonAnswersController : ControllerBase
    {
        private readonly ILessonAnswerSvc _lessonAnswerSvc;
        private readonly IMapper _mapper;

        public LessonAnswersController(ILessonAnswerSvc lessonAnswerSvc, IMapper mapper)
        {
            _lessonAnswerSvc = lessonAnswerSvc;
            _mapper = mapper;
        }
        /// <summary>
        /// để studentId = null
        /// </summary>
        [HttpPost("teacher")]
        public async Task<ActionResult<Logger>> TeacherCreate(LessonAnswerDTO lessonAnswerDTO)
        {
            var lessonAnswer = _mapper.Map<LessonAnswer>(lessonAnswerDTO);

            lessonAnswer.isTeacher = true;

            var loggerResult = await _lessonAnswerSvc.Create(lessonAnswer);
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
        public async Task<ActionResult<Logger>> StudentCreate(LessonAnswerDTO lessonAnswerDTO)
        {
            var lessonAnswer = _mapper.Map<LessonAnswer>(lessonAnswerDTO);

            lessonAnswer.isTeacher = false;

            var loggerResult = await _lessonAnswerSvc.Create(lessonAnswer);
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
            var loggerResult = await _lessonAnswerSvc.GetAll();
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
                var loggerResult = await _lessonAnswerSvc.GetById(id);
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

        [HttpGet("question/{id}")]
        public async Task<ActionResult<Logger>> GetByLessonId(int id)
        {
            if (!String.IsNullOrWhiteSpace(id.ToString()))
            {
                var loggerResult = await _lessonAnswerSvc.GetByQuestionId(id);
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
        public async Task<ActionResult<Logger>> Update(LessonAnswerDTO lessonAnswerDTO)
        {
            var lessonAnswer = _mapper.Map<LessonAnswer>(lessonAnswerDTO);

            var loggerResult = await _lessonAnswerSvc.Update(lessonAnswer);
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
                var loggerResult = await _lessonAnswerSvc.Delete(id);
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
