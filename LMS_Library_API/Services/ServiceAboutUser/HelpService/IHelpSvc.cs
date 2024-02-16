using LMS_Library_API.Models;
using LMS_Library_API.Models.AboutUser;
using LMS_Library_API.Models.RoleAccess;

namespace LMS_Library_API.Services.ServiceAboutUser.HelpService
{
    public interface IHelpSvc
    {
        Task<Logger> Create(Help help);
        Task<Logger> Delete(int helpId);
        Task<Logger> GetByUserId(string userId);
        Task<Logger> GetById(int helpId);
        Task<Logger> GetAll();
    }
}
