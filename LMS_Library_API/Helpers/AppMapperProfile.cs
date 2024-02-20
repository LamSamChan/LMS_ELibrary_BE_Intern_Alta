using AutoMapper;
using LMS_Library_API.Models;
using LMS_Library_API.Models.AboutUser;
using LMS_Library_API.Models.RoleAccess;
using LMS_Library_API.ModelsDTO;
using System.Net;

namespace LMS_Library_API.Helpers
{
    public class AppMapperProfile:Profile
    {
        public AppMapperProfile()
        {
            CreateMap<RoleDTO, Role>();
            CreateMap<Role_PermissionsDTO, Role_Permissions>();
            CreateMap<UserDTO, User>();
            CreateMap<HelpDTO, Help>();
            CreateMap<PrivateFileDTO, PrivateFile>();
            CreateMap<PUTPrivateFileDTO, PrivateFile>();
        }
    }
}
