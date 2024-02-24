using LMS_Library_API.Models;
using LMS_Library_API.Models.Exams;

namespace LMS_Library_API.Services.ServiceAboutExam.Question_ExamService
{
    public interface IQuestionExamSvc
    {
        Task<Logger> Create(Question_Exam questionExam);
        Task<Logger> Update(Question_Exam questionExam);
        Task<Logger> Delete(string examId, int quesionId);
        Task<Logger> GetById(string examId, int quesionId);
        Task<Logger> CheckFormat(string examId, int quesionId);
        Task<Logger> GetAll();
    }
}
