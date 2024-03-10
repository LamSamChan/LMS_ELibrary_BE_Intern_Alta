using LMS_Library_API.Models;
using LMS_Library_API.ViewModels;

namespace LMS_Library_API.Services.AuthService
{
    public interface IAuthSvc
    {
        Task<Logger> IsStudentLogin(LoginVM loginVM);
        Task<Logger> IsUserLogin(LoginVM loginVM);
    }
}
