using AutoMapper;
using LMS_Library_API.Models;
using LMS_Library_API.Models.AboutUser;
using LMS_Library_API.Models.Exams;
using LMS_Library_API.ModelsDTO;
using LMS_Library_API.Services.ServiceAboutUser.ExamRecentViewsService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LMS_Library_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamRecentViewsController : ControllerBase
    {
        private readonly IExamRecentViewsSvc _examRecentViewsSvc;
        private readonly IMapper _mapper;

        public ExamRecentViewsController(IExamRecentViewsSvc examRecentViewsSvc, IMapper mapper)
        {
            _examRecentViewsSvc = examRecentViewsSvc;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<Logger>> Create(ExamRecentViewDTO examRecentViewDTO)
        {
            var newView = _mapper.Map<ExamRecentViews>(examRecentViewDTO);
            var loggerResult = await _examRecentViewsSvc.Create(newView);
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
            var loggerResult = await _examRecentViewsSvc.GetAll();
            if (loggerResult.status == TaskStatus.RanToCompletion)
            {
                return Ok(loggerResult);
            }
            else
            {
                return BadRequest(loggerResult);
            }
        }

        [HttpGet("{userId}/{examId}")]
        public async Task<ActionResult<Logger>> GetById(string userId, string examId)
        {
            if (!String.IsNullOrWhiteSpace(userId) && !String.IsNullOrWhiteSpace(examId))
            {
                var loggerResult = await _examRecentViewsSvc.GetById(userId, examId);

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


        [HttpGet("user/{userId}")]
        public async Task<ActionResult<Logger>> GetByUserId(string userId)
        {
            if (!String.IsNullOrWhiteSpace(userId))
            {
                var loggerResult = await _examRecentViewsSvc.GetByUserId(userId);

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
    }
}
