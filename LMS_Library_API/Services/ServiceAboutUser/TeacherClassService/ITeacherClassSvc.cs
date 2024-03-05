using LMS_Library_API.Models;
using LMS_Library_API.Models.AboutStudent;
using LMS_Library_API.Models.AboutUser;

namespace LMS_Library_API.Services.ServiceAboutUser.TeacherClassService
{
    public interface ITeacherClassSvc
    {
        Task<Logger> Create(TeacherClass teacherClass);
        Task<Logger> Update(TeacherClass teacherClass);
        Task<Logger> Delete(string userId, string classId);
        Task<Logger> GetById(string userId, string classId);
        Task<Logger> GetByTeacherId(string userId);
        Task<Logger> GetAll();
    }
}
