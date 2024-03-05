using LMS_Library_API.Models;
using LMS_Library_API.Models.AboutSubject;

namespace LMS_Library_API.Services.ServiceAboutSubject.SubjectNotificationService
{
    public interface ISubjectNotificationSvc
    {
        Task<Logger> Create(SubjectNotification subjectNotification);
        Task<Logger> Update(SubjectNotification subjectNotification);
        Task<Logger> Delete(int subjectNotificationId);
        Task<Logger> GetById(int subjectNotificationId);
        Task<Logger> GetBySubjectId(string subjectId);
        Task<Logger> GetByTeacherId(string userId);
        Task<Logger> GetAll();
    }
}
