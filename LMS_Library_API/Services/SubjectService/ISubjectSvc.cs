using LMS_Library_API.Models;
using LMS_Library_API.Models.Notification;

namespace LMS_Library_API.Services.SubjectService
{
    public interface ISubjectSvc
    {
        Task<Logger> Create(Subject subject);
        Task<Logger> Update(Subject subject);
        Task<Logger> Delete(string subjectId);
        Task<Logger> GetById(string subjectId);
        Task<Logger> GetAll();
        Task<Logger> Search(string query);
    }
}
