using LMS_Library_API.Models;
using LMS_Library_API.Models.AboutStudent;

namespace LMS_Library_API.Services.ServiceAboutStudent.StudentQnALikeService
{
    public interface IStudentQnALikesService
    {
        Task<Logger> Create(StudentQnALikes qnALike);
        Task<Logger> Update(StudentQnALikes qnALike);
        Task<Logger> GetByStudentId(string studentId);
        Task<IEnumerable<StudentQnALikes>> GetAll();
    }
}
