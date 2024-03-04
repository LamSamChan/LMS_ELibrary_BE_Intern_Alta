using LMS_Library_API.Models;
using LMS_Library_API.Models.Notification;
using LMS_Library_API.Models.StudentNotification;

namespace LMS_Library_API.Services.ServiceAboutNotification.Stu_NotificationFeatuesService
{
    public interface IStuNotificationFeatuesSvc
    {
        Task<Logger> Create(StudentNotificationFeatures notificationFeature);
        Task<Logger> Update(StudentNotificationFeatures notificationFeature);
        Task<Logger> Delete(int id);
        Task<Logger> GetById(int id);
        Task<Logger> GetAll();
    }
}
