using AutoMapper;
using LMS_Library_API.Models;
using LMS_Library_API.Models.Notification;
using LMS_Library_API.Models.StudentNotification;
using LMS_Library_API.ModelsDTO;
using LMS_Library_API.Services.ServiceAboutNotification.Stu_NotificationSettingService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LMS_Library_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentNotificationFeaturesController : ControllerBase
    {
        private readonly IStuNotificationSettingSvc _stuNotificationSettingSvc;
        private readonly IMapper _mapper;

        public StudentNotificationFeaturesController(IStuNotificationSettingSvc stuNotificationSettingSvc, IMapper mapper)
        {
            _stuNotificationSettingSvc = stuNotificationSettingSvc;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<Logger>> Create(StuNotificationSettingDTO settingDTO)
        {
            var setting = _mapper.Map<StudentNotificationSetting>(settingDTO);

            var loggerResult = await _stuNotificationSettingSvc.Create(setting);
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
            var loggerResult = await _stuNotificationSettingSvc.GetAll();
            if (loggerResult.status == TaskStatus.RanToCompletion)
            {
                return Ok(loggerResult);
            }
            else
            {
                return BadRequest(loggerResult);
            }
        }

        [HttpGet("{studentId}/{id}")]
        public async Task<ActionResult<Logger>> GetById(string studentId, int id)
        {
            if (!String.IsNullOrWhiteSpace(id.ToString()))
            {
                var loggerResult = await _stuNotificationSettingSvc.GetById(studentId, id);
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

        [HttpGet("user/{studentId}")]
        public async Task<ActionResult<Logger>> GetByUserId(string studentId)
        {
            if (!String.IsNullOrWhiteSpace(studentId))
            {
                var loggerResult = await _stuNotificationSettingSvc.GetByStudentId(studentId);
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
        public async Task<ActionResult<Logger>> Update(StuNotificationSettingDTO settingDTO)
        {
            var setting = _mapper.Map<StudentNotificationSetting>(settingDTO);

            var loggerResult = await _stuNotificationSettingSvc.Update(setting);
            if (loggerResult.status == TaskStatus.RanToCompletion)
            {
                return Ok(loggerResult);
            }
            else
            {
                return BadRequest(loggerResult);
            }
        }

        [HttpDelete("{studentId}/{id}")]
        public async Task<ActionResult<Logger>> Delete(string studentId, int id)
        {
            if (!String.IsNullOrWhiteSpace(id.ToString()))
            {
                var loggerResult = await _stuNotificationSettingSvc.Delete(studentId, id);
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
