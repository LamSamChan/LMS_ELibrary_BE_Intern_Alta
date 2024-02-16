using LMS_Library_API.Models;
using LMS_Library_API.Models.AboutUser;

namespace LMS_Library_API.Services.ServiceAboutUser.QnALikesService
{
    public interface IQnALikesSvc
    {
        Task<Logger> Create(QnALikes qnALike);
        Task<Logger> Update(QnALikes qnALike);
        Task<Logger> GetByUserId(string userId);
        Task<IEnumerable<QnALikes>> GetAll();
    }
}
