using LMS_Library_API.Models;
using LMS_Library_API.Models.AboutStudent;
using LMS_Library_API.Models.AboutUser;

namespace LMS_Library_API.Services.ServiceAboutStudent.StudentQuestionLikeService
{
    public interface IStudentQuestionLikeSvc
    {
        Task<Logger> Create(StudentQuestionLike questionLike);
        Task<Logger> Delete(StudentQuestionLike existQuestionLike);
        Task<Logger> GetAll();
        Task<Logger> GetByStudentLesson(string studentId, int lessonId);
    }
}
