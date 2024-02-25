using LMS_Library_API.Models;

namespace LMS_Library_API.Services.StudentService
{
    public interface IStudentSvc
    {
        Task<Logger> Create(Student student);
        Task<Logger> Update(Student student);
        Task<Logger> GetById(string studentId);
        Task<Logger> GetAll();
        Task<Logger> Search(string query);
    }
}
