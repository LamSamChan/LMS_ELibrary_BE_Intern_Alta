using AutoMapper;
using LMS_Library_API.Models;
using LMS_Library_API.Models.Notification;
using LMS_Library_API.ModelsDTO;
using LMS_Library_API.Services.ServiceAboutNotification.NotificationSettingService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace LMS_Library_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationSettingController : ControllerBase
    {
        private readonly INotificationSettingSvc _notificationSettingSvc;
        private readonly IMapper _mapper;

        public NotificationSettingController(INotificationSettingSvc notificationSettingSvc, IMapper mapper)
        {
            _notificationSettingSvc = notificationSettingSvc;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<Logger>> Create(NotificationSettingDTO settingDTO)
        {
            var setting = _mapper.Map<NotificationSetting>(settingDTO);

            var loggerResult = await _notificationSettingSvc.Create(setting);
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
            var loggerResult = await _notificationSettingSvc.GetAll();
            if (loggerResult.status == TaskStatus.RanToCompletion)
            {
                return Ok(loggerResult);
            }
            else
            {
                return BadRequest(loggerResult);
            }
        }

        [HttpGet("{userId}/{id}")]
        public async Task<ActionResult<Logger>> GetById(string userId, int id)
        {
            if (!String.IsNullOrWhiteSpace(id.ToString()))
            {
                var loggerResult = await _notificationSettingSvc.GetById(userId, id);
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
                var loggerResult = await _notificationSettingSvc.GetByUserId(userId);
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
        public async Task<ActionResult<Logger>> Update(NotificationSettingDTO settingDTO)
        {
            var setting = _mapper.Map<NotificationSetting>(settingDTO);

            var loggerResult = await _notificationSettingSvc.Update(setting);
            if (loggerResult.status == TaskStatus.RanToCompletion)
            {
                return Ok(loggerResult);
            }
            else
            {
                return BadRequest(loggerResult);
            }
        }

        [HttpDelete("{userId}/{id}")]
        public async Task<ActionResult<Logger>> Delete(string userId, int id)
        {
            if (!String.IsNullOrWhiteSpace(id.ToString()))
            {
                var loggerResult = await _notificationSettingSvc.Delete(userId, id);
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
