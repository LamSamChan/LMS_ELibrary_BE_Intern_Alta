using LMS_Library_API.Models;
using LMS_Library_API.Models.RoleAccess;

namespace LMS_Library_API.Services.RoleAccess.RoleService
{
    public interface IRoleSvc
    {
        Task<Logger> Create(Role role);
        Task<Logger> Update(Role role);
        Task<Logger> Delete(int roleId);
        Task<Logger> GetById(int roleId);
        Task<Logger> GetAll();
    }
}
