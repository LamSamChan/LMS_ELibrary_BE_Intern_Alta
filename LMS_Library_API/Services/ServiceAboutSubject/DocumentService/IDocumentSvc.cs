using LMS_Library_API.Models;
using LMS_Library_API.Models.AboutSubject;

namespace LMS_Library_API.Services.ServiceAboutSubject.DocumentService
{
    public interface IDocumentSvc
    {
        Task<Logger> Create(Document document);
        Task<Logger> Update(Document document);
        Task<Logger> Delete(int documentId);
        Task<Logger> GetById(int documentId);
        Task<Logger> GetAll();
    }
}
