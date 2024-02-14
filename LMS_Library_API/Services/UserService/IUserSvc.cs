using LMS_Library_API.Models;
using LMS_Library_API.Models.RoleAccess;

namespace LMS_Library_API.Services.UserService
{
    public interface IUserSvc
    {
        Task<Logger> Create(User user);
        Task<Logger> Update(User user);
        Task<Logger> GetById(string userId);
        Task<Logger> GetAll();
        Task<Logger> Search(string query);

    }
}
