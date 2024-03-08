using LMS_Library_API.Models;
using LMS_Library_API.Models.AboutStudent;
using LMS_Library_API.Models.AboutSubject;

namespace LMS_Library_API.Services.ServiceAboutSubject.NotificationClassStudentService
{
    public interface INotificationClassStudentSvc
    {
        Task<Logger> Create(NotificationClassStudent notification);
        Task<Logger> Update(NotificationClassStudent notification);
        Task<Logger> Delete(int subjectNotificationId, string classId, Guid? studentId);
        Task<Logger> GetById(int subjectNotificationId, string classId, Guid? studentId);
        Task<Logger> GetByClassId(string classId);
        Task<Logger> GetByStudentId(string studentId);
        Task<Logger> GetAll();
    }
}
