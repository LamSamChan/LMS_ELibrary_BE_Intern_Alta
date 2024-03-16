using LMS_Library_API.Models;
using LMS_Library_API.Models.AboutStudent;
using LMS_Library_API.Models.Notification;

namespace LMS_Library_API.Services.ServiceAboutSubject.ClassSubjectService
{
    public interface IClassSubjectSvc
    {
        Task<Logger> Create(ClassSubject classSubject);
        Task<Logger> Delete(ClassSubject classSubject);
        Task<Logger> GetById(string classId, string subjectId);
        Task<Logger> GetByClassId(string classId);
        Task<Logger> GetAll();
    }
}
