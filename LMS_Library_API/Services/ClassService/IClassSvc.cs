using LMS_Library_API.Models;

namespace LMS_Library_API.Services.ClassService
{
    public interface IClassSvc
    {
        Task<Logger> Create(Class classModel);
        Task<Logger> Update(Class classModel);
        Task<Logger> Delete(string classId);
        Task<Logger> GetById(string classId);
        Task<int> CountStudentInClass(string classId);
        Task<Logger> GetAll();
        Task<Logger> Search(string query);
    }
}
