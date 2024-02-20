using LMS_Library_API.Models;
using LMS_Library_API.Models.AboutUser;

namespace LMS_Library_API.Services.ServiceAboutUser.PrivateFileService
{
    public interface IPrivateFileSvc
    {
        Task<Logger> Create(PrivateFile privateFile);
        Task<Logger> GetAll();
        Task<Logger> GetById(int fileId);
        Task<Logger> GetByUserId(string userId);
        Task<Logger> Search(string userId, string query);
        Task<Logger> Update(PrivateFile privateFile);
        Task<Logger> Delete(int fileId);
    }
}
