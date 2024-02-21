using LMS_Library_API.Models;
using LMS_Library_API.Models.AboutUser;
using LMS_Library_API.Models.Notification;

namespace LMS_Library_API.Services.ServiceAboutNotification.NotificationService
{
    public interface INotificationSvc
    {
        Task<Logger> Create(Notification notification);
        Task<Logger> GetAll();
        Task<Logger> GetById(int notificationId);
        Task<Logger> GetByRecipientId(string userId);
        Task<Logger> Search(string userId, string query);
        Task<Logger> Delete(int notificationId);
        Task<Logger> Update(Notification notification);
    }
}
