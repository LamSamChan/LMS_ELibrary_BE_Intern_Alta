using AutoMapper;
using LMS_Library_API.Models;
using LMS_Library_API.Models.AboutStudent;
using LMS_Library_API.Models.AboutSubject;
using LMS_Library_API.ModelsDTO;
using LMS_Library_API.Services.ServiceAboutSubject.NotificationClassStudentService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LMS_Library_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationClassStudentsController : ControllerBase
    {
        private readonly INotificationClassStudentSvc _notificationClassStudentSvc;
        private readonly IMapper _mapper;

        public NotificationClassStudentsController(INotificationClassStudentSvc notificationClassStudentSvc, IMapper mapper)
        {
            _mapper = mapper;
            _notificationClassStudentSvc = notificationClassStudentSvc;
        }

        [HttpPost]
        public async Task<ActionResult<Logger>> Create(NotificationClassStudentDTO notificationClassStudentDTO)
        {
            var notification = _mapper.Map<NotificationClassStudent>(notificationClassStudentDTO);

            var loggerResult = await _notificationClassStudentSvc.Create(notification);
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
            var loggerResult = await _notificationClassStudentSvc.GetAll();
            if (loggerResult.status == TaskStatus.RanToCompletion)
            {
                return Ok(loggerResult);
            }
            else
            {
                return BadRequest(loggerResult);
            }
        }

        [HttpGet("{subjectNotificationId}/{classId}")]
        public async Task<ActionResult<Logger>> GetById(int subjectNotificationId, string classId, Guid? studentId)
        {
            if (!String.IsNullOrWhiteSpace(subjectNotificationId.ToString()) || !String.IsNullOrWhiteSpace(classId))
            {
                var loggerResult = await _notificationClassStudentSvc.GetById(subjectNotificationId, classId, studentId);
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

        [HttpGet("class/{classId}")]
        public async Task<ActionResult<Logger>> GetByClassId(string classId)
        {
            if (!String.IsNullOrWhiteSpace(classId))
            {
                var loggerResult = await _notificationClassStudentSvc.GetByClassId(classId);
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

        [HttpGet("student/{studentId}")]
        public async Task<ActionResult<Logger>> GetByStudentId(string studentId)
        {
            if (!String.IsNullOrWhiteSpace(studentId))
            {
                var loggerResult = await _notificationClassStudentSvc.GetByStudentId(studentId);
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
        public async Task<ActionResult<Logger>> Update(NotificationClassStudentDTO notificationClassStudentDTO)
        {
            var notification = _mapper.Map<NotificationClassStudent>(notificationClassStudentDTO);

            var loggerResult = await _notificationClassStudentSvc.Update(notification);
            if (loggerResult.status == TaskStatus.RanToCompletion)
            {
                return Ok(loggerResult);
            }
            else
            {
                return BadRequest(loggerResult);
            }
        }

        [HttpDelete("{subjectNotificationId}/{classId}")]
        public async Task<ActionResult<Logger>> Delete(int subjectNotificationId, string classId, Guid? studentId)
        {
            if (!String.IsNullOrWhiteSpace(subjectNotificationId.ToString()) || !String.IsNullOrWhiteSpace(classId))
            {
                var loggerResult = await _notificationClassStudentSvc.Delete(subjectNotificationId, classId, studentId);
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
