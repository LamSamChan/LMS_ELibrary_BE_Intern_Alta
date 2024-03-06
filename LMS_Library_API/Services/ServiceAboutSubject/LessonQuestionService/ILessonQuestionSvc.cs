using LMS_Library_API.Models;
using LMS_Library_API.Models.AboutStudent;
using LMS_Library_API.Models.AboutSubject;

namespace LMS_Library_API.Services.ServiceAboutSubject.LessonQuestionService
{
    public interface ILessonQuestionSvc
    {
        Task<Logger> Create(LessonQuestion lessonQuestion);
        Task<Logger> Update(LessonQuestion lessonQuestion);
        Task<Logger> Delete(int id);
        Task<Logger> GetById(int id);
        Task<Logger> GetByLessonId(int lessonId);
        Task<Logger> GetAll();
    }
}
