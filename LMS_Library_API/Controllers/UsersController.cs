using AutoMapper;
using LMS_Library_API.Models;
using LMS_Library_API.Models.AboutUser;
using LMS_Library_API.Models.RoleAccess;
using LMS_Library_API.ModelsDTO;
using LMS_Library_API.Services.ServiceAboutUser.QnALikesService;
using LMS_Library_API.Services.UserService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LMS_Library_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserSvc _userSvc;
        private readonly IMapper _mapper;
        private readonly IQnALikesSvc _qnALikesSvc;
        public UsersController (IUserSvc userSvc, IMapper mapper, IQnALikesSvc qnALikesSvc) {
            _mapper = mapper;
            _userSvc = userSvc;
            _qnALikesSvc = qnALikesSvc;
        }

        [HttpPost]
        public async Task<ActionResult<Logger>> Create(UserDTO user)
        {
            var newUser = _mapper.Map<User>(user);
            var loggerResult = await _userSvc.Create(newUser);
            if (loggerResult.status == TaskStatus.RanToCompletion)
            {
                User createQnALike = (User)loggerResult.data;

                QnALikes qnALikes = new QnALikes() { UserId = createQnALike.Id, QuestionsLikedID="[]", AnswersLikedID="[]" };
                var qnaResult = await _qnALikesSvc.Create(qnALikes);

                if (qnaResult.status == TaskStatus.RanToCompletion)
                {
                    return Ok(loggerResult);
                }
                else
                {
                    return BadRequest(qnaResult);

                }
            }
            else
            {
                return BadRequest(loggerResult);
            }
        }

        [HttpGet]
        public async Task<ActionResult<Logger>> GetAll()
        {
            var loggerResult = await _userSvc.GetAll();
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
        public async Task<ActionResult<Logger>> GetById(string id)
        {
            if (!String.IsNullOrWhiteSpace(id))
            {
                var loggerResult = await _userSvc.GetById(id);
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
        public async Task<ActionResult<Logger>> Update(UserDTO user)
        {
            var newDataUser = _mapper.Map<User>(user);

            var loggerResult = await _userSvc.Update(newDataUser);
            if (loggerResult.status == TaskStatus.RanToCompletion)
            {
                return Ok(loggerResult);
            }
            else
            {
                return BadRequest(loggerResult);
            }
        }

        [HttpGet("search/{query}")]
        public async Task<ActionResult<Logger>> Search(string query)
        {
            if (!String.IsNullOrWhiteSpace(query))
            {
                var loggerResult = await _userSvc.Search(query.Trim().ToUpper());
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
