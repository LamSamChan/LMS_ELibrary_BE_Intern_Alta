using LMS_Library_API.Models;
using LMS_Library_API.Models.AboutSubject;
using LMS_Library_API.Models.Exams;

namespace LMS_Library_API.Services.ExamService
{
    public interface IExamSvc
    {
        Task<Logger> Create(Exam exam);
        Task<Logger> Update(Exam exam);
        Task<Logger> Delete(string examId);
        Task<Logger> GetDetailExam(string examId, bool format);
        Task<Logger> CreateMCExamByFile(string filePath);
        Task<Logger> GetById(string examId);
        Task<Logger> Search(string query);
        Task<Logger> GetAll();
    }
}
