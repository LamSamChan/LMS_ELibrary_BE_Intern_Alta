using LMS_Library_API.Models;
using LMS_Library_API.Models.RoleAccess;

namespace LMS_Library_API.Services.RoleAccess.PermissionsService
{
    public interface IPermissionsSvc
    {
        Task<Logger> Create(Permissions permissions);
        Task<Logger> Update(Permissions permissions);
        Task<Logger> Delete(int permissionsId);
        Task<Logger> GetById(int permissionsId);
        Task<Logger> GetAll();
    }
}
