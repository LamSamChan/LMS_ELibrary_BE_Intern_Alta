using LMS_Library_API.Models;
using LMS_Library_API.Models.AboutStudent;
using LMS_Library_API.Models.Notification;

namespace LMS_Library_API.Services.ServiceAboutStudent.StudyTimeService
{
    public interface IStudyTimeSvc
    {
        Task<Logger> Create(StudyTime studyTime);
        Task<Logger> Update(StudyTime studyTime);
        Task<Logger> Delete(string studentId, string subjectId, DateTime dateStudy);
        Task<Logger> GetById(string studentId, string subjectId, DateTime dateStudy);
        Task<Logger> GetByStudentId(string studentId);
        Task<Logger> GetAll();
    }
}
