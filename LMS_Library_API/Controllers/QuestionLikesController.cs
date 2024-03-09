using AutoMapper;
using LMS_Library_API.Models;
using LMS_Library_API.Models.AboutUser;
using LMS_Library_API.Models.Exams;
using LMS_Library_API.ModelsDTO;
using LMS_Library_API.Services.ServiceAboutUser.QuestionLikeService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LMS_Library_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionLikesController : ControllerBase
    {
        private readonly IQuestionLikeSvc _questionLikeSvc;
        private readonly IMapper _mapper;

        public QuestionLikesController(IQuestionLikeSvc questionLikeSvc, IMapper mapper)
        {
            _mapper = mapper;
            _questionLikeSvc = questionLikeSvc;
        }

        [HttpPost]
        public async Task<ActionResult<Logger>> Create(QuestionLikeDTO questionLikeDTO)
        {
            var like = _mapper.Map<QuestionLike>(questionLikeDTO);

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

        [HttpGet("userLesson/{userId}/{lessonId}")]
        public async Task<ActionResult<Logger>> GetByUserLesson(string userId, int lessonId)
        {
            if (!String.IsNullOrWhiteSpace(userId) || !String.IsNullOrWhiteSpace(lessonId.ToString()))
            {
                var loggerResult = await _questionLikeSvc.GetByUserLesson(userId, lessonId);
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
        public async Task<ActionResult<Logger>> Delete(QuestionLikeDTO questionLikeDTO)
        {
            var existLike = _mapper.Map<QuestionLike>(questionLikeDTO);

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
