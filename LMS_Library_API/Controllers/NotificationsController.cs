using AutoMapper;
using LMS_Library_API.Models;
using LMS_Library_API.Models.Notification;
using LMS_Library_API.ModelsDTO;
using LMS_Library_API.Services.ServiceAboutNotification.NotificationService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LMS_Library_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class NotificationsController : ControllerBase
    {
        private readonly INotificationSvc _notificationSvc;
        private readonly IMapper _mapper;

        public NotificationsController(INotificationSvc notificationSvc, IMapper mapper)
        {
            _notificationSvc = notificationSvc;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<Logger>> Create(NotificationDTO notification)
        {
            var newNotify = _mapper.Map<Notification>(notification);

            var loggerResult = await _notificationSvc.Create(newNotify);
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
            var loggerResult = await _notificationSvc.GetAll();
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
                var loggerResult = await _notificationSvc.GetById(id);
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

        [HttpGet("user/{id}")]
        public async Task<ActionResult<Logger>> GetByUserId(string id)
        {
            if (!String.IsNullOrWhiteSpace(id))
            {
                var loggerResult = await _notificationSvc.GetByTeacherRecipientId(id);
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

        [HttpGet("searchByTeacher/{userId}/{query}")]
        public async Task<ActionResult<Logger>> Search(string userId, string query)
        {
            if (!String.IsNullOrWhiteSpace(query))
            {
                var loggerResult = await _notificationSvc.SearchTeacherRecipient(userId, query);
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

        [HttpGet("student/{id}")]
        public async Task<ActionResult<Logger>> GetByStudentId(string id)
        {
            if (!String.IsNullOrWhiteSpace(id))
            {
                var loggerResult = await _notificationSvc.GetByStudentRecipientId(id);
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

        [HttpGet("searchByStudent/{studentId}/{query}")]
        public async Task<ActionResult<Logger>> SearchByStudent(string studentId, string query)
        {
            if (!String.IsNullOrWhiteSpace(query))
            {
                var loggerResult = await _notificationSvc.SearchStudentRecipient(studentId, query);
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

        [HttpPut]
        public async Task<ActionResult<Logger>> Update(NotificationDTO notificationDTO)
        {
            var update = _mapper.Map<Notification>(notificationDTO);

            var loggerResult = await _notificationSvc.Update(update);
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
        public async Task<ActionResult<Logger>> Detele(int id)
        {
            if (!String.IsNullOrWhiteSpace(id.ToString()))
            {
                var loggerResult = await _notificationSvc.Delete(id);
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
