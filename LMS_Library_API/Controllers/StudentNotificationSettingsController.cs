using LMS_Library_API.Models;
using LMS_Library_API.Models.Notification;
using LMS_Library_API.Models.StudentNotification;
using LMS_Library_API.Services.ServiceAboutNotification.NotificationFeaturesService;
using LMS_Library_API.Services.ServiceAboutNotification.Stu_NotificationFeatuesService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LMS_Library_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class StudentNotificationSettingsController : ControllerBase
    {
        private readonly IStuNotificationFeatuesSvc _stuNotificationFeaturesSvc;

        public StudentNotificationSettingsController(IStuNotificationFeatuesSvc stuNotificationFeaturesSvc)
        {
            _stuNotificationFeaturesSvc = stuNotificationFeaturesSvc;
        }

        [HttpPost]
        public async Task<ActionResult<Logger>> Create(StudentNotificationFeatures features)
        {
            var loggerResult = await _stuNotificationFeaturesSvc.Create(features);
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
            var loggerResult = await _stuNotificationFeaturesSvc.GetAll();
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
                var loggerResult = await _stuNotificationFeaturesSvc.GetById(id);
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
        public async Task<ActionResult<Logger>> Update(StudentNotificationFeatures features)
        {

            var loggerResult = await _stuNotificationFeaturesSvc.Update(features);
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
                var loggerResult = await _stuNotificationFeaturesSvc.Delete(id);
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
