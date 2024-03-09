using AutoMapper;
using LMS_Library_API.Models;
using LMS_Library_API.Models.AboutStudent;
using LMS_Library_API.Models.AboutUser;
using LMS_Library_API.ModelsDTO;
using LMS_Library_API.Services.ServiceAboutStudent.StudentAnswerLikeService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LMS_Library_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentAnswerLikesController : ControllerBase
    {
        private readonly IStudentAnswerLikeSvc _answerLikeSvc;
        private readonly IMapper _mapper;

        public StudentAnswerLikesController(IStudentAnswerLikeSvc answerLikeSvc, IMapper mapper)
        {
            _mapper = mapper;
            _answerLikeSvc = answerLikeSvc;
        }

        [HttpPost]
        public async Task<ActionResult<Logger>> Create(StudentAnswerLikeDTO answerLikeDTO)
        {
            var like = _mapper.Map<StudentAnswerLike>(answerLikeDTO);

            var loggerResult = await _answerLikeSvc.Create(like);
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
            var loggerResult = await _answerLikeSvc.GetAll();
            if (loggerResult.status == TaskStatus.RanToCompletion)
            {
                return Ok(loggerResult);
            }
            else
            {
                return BadRequest(loggerResult);
            }
        }

        [HttpGet("studentLesson/{studentId}/{lessonId}")]
        public async Task<ActionResult<Logger>> GetByUserLesson(string studentId, int lessonId)
        {
            if (!String.IsNullOrWhiteSpace(studentId) || !String.IsNullOrWhiteSpace(lessonId.ToString()))
            {
                var loggerResult = await _answerLikeSvc.GetByStudentLesson(studentId, lessonId);
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

        [HttpDelete]
        public async Task<ActionResult<Logger>> Delete(StudentAnswerLikeDTO answerLikeDTO)
        {
            var existLike = _mapper.Map<StudentAnswerLike>(answerLikeDTO);

            var loggerResult = await _answerLikeSvc.Delete(existLike);
            if (loggerResult.status == TaskStatus.RanToCompletion)
            {
                return Ok(loggerResult);
            }
            else
            {
                return BadRequest(loggerResult);
            }
        }
    }
}
