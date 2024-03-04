using LMS_Library_API.Models;
using LMS_Library_API.Models.Notification;
using LMS_Library_API.Models.StudentNotification;

namespace LMS_Library_API.Services.ServiceAboutNotification.Stu_NotificationSettingService
{
    public interface IStuNotificationSettingSvc
    {
        Task<Logger> Create(StudentNotificationSetting setting);
        Task<Logger> Update(StudentNotificationSetting setting);
        Task<Logger> Delete(string studentId, int id);
        Task<Logger> GetById(string studentId, int id);
        Task<Logger> GetByStudentId(string studentId);
        Task<Logger> GetAll();
    }
}
