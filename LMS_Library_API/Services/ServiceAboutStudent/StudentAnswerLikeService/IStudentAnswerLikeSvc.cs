using LMS_Library_API.Models;
using LMS_Library_API.Models.AboutStudent;

namespace LMS_Library_API.Services.ServiceAboutStudent.StudentAnswerLikeService
{
    public interface IStudentAnswerLikeSvc
    {
        Task<Logger> Create(StudentAnswerLike answerLike);
        Task<Logger> Delete(StudentAnswerLike existAnswerLike);
        Task<Logger> GetAll();
        Task<Logger> GetByStudentLesson(string studnetId, int lessonId);
    }
}
