using LMS_Library_API.Models;
using LMS_Library_API.Models.AboutUser;

namespace LMS_Library_API.Services.ServiceAboutUser.QuestionLikeService
{
    public interface IQuestionLikeSvc
    {
        Task<Logger> Create(QuestionLike questionLike);
        Task<Logger> Delete(QuestionLike existQuestionLike);
        Task<Logger> GetAll();
        Task<Logger> GetByUserLesson(string userId, int lessonId);
    }
}
