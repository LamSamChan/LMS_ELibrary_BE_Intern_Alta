using LMS_Library_API.Models;
using LMS_Library_API.Models.AboutStudent;

namespace LMS_Library_API.Services.ServiceAboutStudent.StudentSubjectService
{
    public interface IStudentSubjectSvc
    {
        Task<Logger> Create(StudentSubject studentSubject);
        Task<Logger> Update(StudentSubject studentSubject);
        Task<Logger> Delete(string studentId, string subjectId);
        Task<Logger> GetById(string studentId, string subjectId);
        Task<Logger> GetByStudentId(string studentId);
        Task<Logger> GetAll();
    }
}
