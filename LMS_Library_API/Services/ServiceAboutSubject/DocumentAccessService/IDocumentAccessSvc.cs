using LMS_Library_API.Models;
using LMS_Library_API.Models.AboutSubject;

namespace LMS_Library_API.Services.ServiceAboutSubject.DocumentAccessService
{
    public interface IDocumentAccessSvc
    {
        Task<Logger> Create(DocumentAccess documentAccess);
        Task<Logger> Update(DocumentAccess documentAccess);
        Task<Logger> Delete(int documentId);
        Task<Logger> GetById(int documentId);
        Task<Logger> GetByClassId(string classId);
        Task<Logger> GetAll();
    }
}
