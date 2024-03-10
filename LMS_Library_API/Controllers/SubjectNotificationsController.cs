using AutoMapper;
using LMS_Library_API.Models;
using LMS_Library_API.Models.AboutSubject;
using LMS_Library_API.Models.AboutUser;
using LMS_Library_API.ModelsDTO;
using LMS_Library_API.Services.ServiceAboutSubject.SubjectNotificationService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LMS_Library_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SubjectNotificationsController : ControllerBase
    {
        private readonly ISubjectNotificationSvc _subjectNotificationSvc;
        private readonly IMapper _mapper;

        public SubjectNotificationsController(ISubjectNotificationSvc subjectNotificationSvc, IMapper mapper)
        {
            _mapper = mapper;
            _subjectNotificationSvc = subjectNotificationSvc;
        }

        [HttpPost]
        public async Task<ActionResult<Logger>> Create(SubjectNotificationDTO subjectNotificationDTO)
        {
            var notification = _mapper.Map<SubjectNotification>(subjectNotificationDTO);

            var loggerResult = await _subjectNotificationSvc.Create(notification);
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
            var loggerResult = await _subjectNotificationSvc.GetAll();
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
                var loggerResult = await _subjectNotificationSvc.GetById(id);
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

        [HttpGet("subject/{id}")]
        public async Task<ActionResult<Logger>> GetBySubjectId(string id)
        {
            if (!String.IsNullOrWhiteSpace(id))
            {
                var loggerResult = await _subjectNotificationSvc.GetBySubjectId(id);
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

        [HttpGet("teacher/{id}")]
        public async Task<ActionResult<Logger>> GetByTeacherId(string id)
        {
            if (!String.IsNullOrWhiteSpace(id))
            {
                var loggerResult = await _subjectNotificationSvc.GetByTeacherId(id);
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
        public async Task<ActionResult<Logger>> Update(SubjectNotificationDTO subjectNotificationDTO)
        {
            var notification = _mapper.Map<SubjectNotification>(subjectNotificationDTO);

            var loggerResult = await _subjectNotificationSvc.Update(notification);
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
                var loggerResult = await _subjectNotificationSvc.Delete(id);
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
