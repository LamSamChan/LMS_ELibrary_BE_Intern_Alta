using LMS_Library_API.Models;
using LMS_Library_API.Models.AboutStudent;

namespace LMS_Library_API.Services.ServiceAboutStudent.StudyHistoryService
{
    public interface IStudyHistorySvc
    {
        Task<Logger> Create(StudyHistory studyHistory);
        Task<Logger> Update(StudyHistory studyHistory);
        Task<Logger> Delete(string studentId, int documentId);
        Task<Logger> GetById(string studentId, int documentId);
        Task<Logger> GetByStudentId(string studentId);
        Task<Logger> GetAll();
    }
}
