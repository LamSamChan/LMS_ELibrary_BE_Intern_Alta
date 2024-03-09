using LMS_Library_API.Models;
using LMS_Library_API.Models.AboutUser;

namespace LMS_Library_API.Services.ServiceAboutUser.AnswerLikeService
{
    public interface IAnswerLikeSvc
    {
        Task<Logger> Create(AnswerLike answerLike);
        Task<Logger> Delete(AnswerLike ExistAnswerLike);
        Task<Logger> GetAll();
        Task<Logger> GetByUserLesson(string userId, int lessonId);
    }
}
