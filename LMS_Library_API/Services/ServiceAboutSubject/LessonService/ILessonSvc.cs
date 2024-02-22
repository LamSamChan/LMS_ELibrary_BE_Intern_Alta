using LMS_Library_API.Models;
using LMS_Library_API.Models.AboutSubject;

namespace LMS_Library_API.Services.ServiceAboutSubject.LessonService
{
    public interface ILessonSvc
    {
        Task<Logger> Create(Lesson lesson);
        Task<Logger> Update(Lesson lesson);
        Task<Logger> Delete(int lessonId);
        Task<Logger> GetById(int lessonId);
        Task<Logger> GetAll();
    }
}
