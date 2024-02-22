using LMS_Library_API.Models;
using LMS_Library_API.Models.AboutSubject;

namespace LMS_Library_API.Services.ServiceAboutSubject.PartService
{
    public interface IPartSvc
    {
        Task<Logger> Create(Part part);
        Task<Logger> Update(Part part);
        Task<Logger> Delete(int partId);
        Task<Logger> GetById(int partId);
        Task<Logger> GetAll();
    }
}
