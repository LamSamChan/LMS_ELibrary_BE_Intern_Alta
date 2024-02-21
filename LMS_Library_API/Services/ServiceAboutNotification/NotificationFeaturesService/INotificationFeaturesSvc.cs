using LMS_Library_API.Models;
using LMS_Library_API.Models.Notification;

namespace LMS_Library_API.Services.ServiceAboutNotification.NotificationFeaturesService
{
    public interface INotificationFeaturesSvc
    {
        Task<Logger> Create(NotificationFeatures notificationFeature);
        Task<Logger> Update(NotificationFeatures notificationFeature);
        Task<Logger> Delete(int id);
        Task<Logger> GetById(int id);
        Task<Logger> GetAll();
    }
}
