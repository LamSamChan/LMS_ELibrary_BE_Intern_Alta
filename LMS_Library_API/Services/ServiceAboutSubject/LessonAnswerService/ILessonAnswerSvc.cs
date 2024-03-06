using LMS_Library_API.Models;
using LMS_Library_API.Models.AboutSubject;

namespace LMS_Library_API.Services.ServiceAboutSubject.LessonAnswerService
{
    public interface ILessonAnswerSvc
    {
        Task<Logger> Create(LessonAnswer lessonAnswer);
        Task<Logger> Update(LessonAnswer lessonAnswer);
        Task<Logger> Delete(int id);
        Task<Logger> GetById(int id);
        Task<Logger> GetByQuestionId(int questionId);
        Task<Logger> GetAll();
    }
}
