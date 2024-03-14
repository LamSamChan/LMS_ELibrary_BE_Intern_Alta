using LMS_Library_API.Models;
using LMS_Library_API.Models.Exams;
using LMS_Library_API.Models.RoleAccess;

namespace LMS_Library_API.Services.ServiceAboutExam.QuestionBankService
{
    public interface IQuestionBankSvc
    {
        Task<Logger> Create(QuestionBanks questionBanks);
        Task<Logger> Update(QuestionBanks questionBanks);
        Task<Logger> Delete(int questionBanksId);
        Task<Logger> GetById(int questionBanksId);
        Task<Logger> GetAll();
        Task<ICollection<Question_Exam>> GetRandomQuestion(int easy, int medium, int hard, string subjectId);

    }
}
