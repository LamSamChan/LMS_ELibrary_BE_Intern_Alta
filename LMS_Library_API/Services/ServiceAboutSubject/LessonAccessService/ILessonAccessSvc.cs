// Ignore Spelling: API LMS Svc

using LMS_Library_API.Models;
using LMS_Library_API.Models.AboutStudent;
using LMS_Library_API.Models.AboutSubject;

namespace LMS_Library_API.Services.ServiceAboutSubject.LessonAccessService
{
    public interface ILessonAccessSvc
    {
        Task<Logger> Create(LessonAccess lessonAccess);
        Task<Logger> Update(LessonAccess lessonAccess);
        Task<Logger> Delete(int lessonId);
        Task<Logger> GetById(int lessonId);
        Task<Logger> GetByClassId(string classId);
        Task<Logger> GetAll();
    }
}
