using AutoMapper;
using LMS_Library_API.Models;
using LMS_Library_API.Models.AboutStudent;
using LMS_Library_API.Models.AboutUser;
using LMS_Library_API.ModelsDTO;
using LMS_Library_API.Services.ServiceAboutStudent.StudentQuestionLikeService;
using LMS_Library_API.Services.ServiceAboutUser.QuestionLikeService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LMS_Library_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentQuestionLikesController : ControllerBase
    {
        private readonly IStudentQuestionLikeSvc _questionLikeSvc;
        private readonly IMapper _mapper;

        public StudentQuestionLikesController(IStudentQuestionLikeSvc questionLikeSvc, IMapper mapper)
        {
            _mapper = mapper;
            _questionLikeSvc = questionLikeSvc;
        }

        [HttpPost]
        public async Task<ActionResult<Logger>> Create(StudentQuestionLikeDTO questionLikeDTO)
        {
            var like = _mapper.Map<StudentQuestionLike>(questionLikeDTO);

            var loggerResult = await _questionLikeSvc.Create(like);
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
            var loggerResult = await _questionLikeSvc.GetAll();
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
                var loggerResult = await _questionLikeSvc.GetByStudentLesson(studentId, lessonId);
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
        public async Task<ActionResult<Logger>> Delete(StudentQuestionLikeDTO questionLikeDTO)
        {
            var existLike = _mapper.Map<StudentQuestionLike>(questionLikeDTO);

            var loggerResult = await _questionLikeSvc.Delete(existLike);
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
