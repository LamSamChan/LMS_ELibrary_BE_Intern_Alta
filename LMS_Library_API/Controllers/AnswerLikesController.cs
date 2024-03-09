using AutoMapper;
using LMS_Library_API.Models;
using LMS_Library_API.Models.AboutUser;
using LMS_Library_API.ModelsDTO;
using LMS_Library_API.Services.ServiceAboutUser.AnswerLikeService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LMS_Library_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnswerLikesController : ControllerBase
    {
        private readonly IAnswerLikeSvc _answerLikeSvc;
        private readonly IMapper _mapper;

        public AnswerLikesController(IAnswerLikeSvc answerLikeSvc, IMapper mapper)
        {
            _mapper = mapper;
            _answerLikeSvc = answerLikeSvc;
        }

        [HttpPost]
        public async Task<ActionResult<Logger>> Create(AnswerLikeDTO answerLikeDTO)
        {
            var like = _mapper.Map<AnswerLike>(answerLikeDTO);

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

        [HttpGet("userLesson/{userId}/{lessonId}")]
        public async Task<ActionResult<Logger>> GetByUserLesson(string userId, int lessonId)
        {
            if (!String.IsNullOrWhiteSpace(userId) || !String.IsNullOrWhiteSpace(lessonId.ToString()))
            {
                var loggerResult = await _answerLikeSvc.GetByUserLesson(userId, lessonId);
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
        public async Task<ActionResult<Logger>> Delete(AnswerLikeDTO answerLikeDTO)
        {
            var existLike = _mapper.Map<AnswerLike>(answerLikeDTO);

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
