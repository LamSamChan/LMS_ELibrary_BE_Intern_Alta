using LMS_Library_API.Models;
using LMS_Library_API.Models.Notification;

namespace LMS_Library_API.Services.ServiceAboutNotification.NotificationSettingService
{
    public interface INotificationSettingSvc
    {
        Task<Logger> Create(NotificationSetting setting);
        Task<Logger> Update(NotificationSetting setting);
        Task<Logger> Delete(string userId, int id);
        Task<Logger> GetById(string userId, int id);
        Task<Logger> GetByUserId(string userId);
        Task<Logger> GetAll();
    }
}
