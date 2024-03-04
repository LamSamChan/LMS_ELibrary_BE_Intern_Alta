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
        Task<Logger> GetByTeacherRecipientId(string userId);
        Task<Logger> GetByStudentRecipientId(string studentId);
        Task<Logger> SearchTeacherRecipient(string userId, string query);
        Task<Logger> SearchStudentRecipient(string userId, string query);
        Task<Logger> Delete(int notificationId);
        Task<Logger> Update(Notification notification);
    }
}
