using LMS_Library_API.Models;
using LMS_Library_API.Models.AboutUser;
using LMS_Library_API.Models.Exams;

namespace LMS_Library_API.Services.ServiceAboutUser.ExamRecentViewsService
{
    public interface IExamRecentViewsSvc
    {
        Task<Logger> Create(ExamRecentViews examRecentViews);
        Task<Logger> GetById(string userId, string examId);
        Task<Logger> GetByUserId(string userId);
        Task<Logger> GetAll();
    }
}
