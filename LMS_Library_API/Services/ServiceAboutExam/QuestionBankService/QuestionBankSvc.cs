using LMS_Library_API.Context;
using LMS_Library_API.Models;
using LMS_Library_API.Models.Exams;
using LMS_Library_API.Models.RoleAccess;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace LMS_Library_API.Services.ServiceAboutExam.QuestionBankService
{
    public class QuestionBankSvc:IQuestionBankSvc
    {
        private readonly DataContext _context;

        public QuestionBankSvc(DataContext context)
        {
            _context = context;
        }

        public async Task<Logger> Create(QuestionBanks questionBanks)
        {
            try
            {
                _context.QuestionBanks.Add(questionBanks);
                await _context.SaveChangesAsync();
                return new Logger()
                {
                    status = TaskStatus.RanToCompletion,
                    message = "Thêm thành công",
                    data = questionBanks
                };
            }
            catch (Exception ex)
            {
                return new Logger()
                {
                    status = TaskStatus.Faulted,
                    message = ex.Message,

                };
            }
        }

        public async Task<Logger> Delete(int questionBanksId)
        {
            try
            {
                var existQuestion = await _context.QuestionBanks
                    .Include(_ => _.QB_Answers_MC)
                    .Include(_ => _.QB_Answer_Essay)
                    .FirstOrDefaultAsync(_ => _.Id == questionBanksId);

                if (existQuestion == null)
                {
                    return new Logger()
                    {
                        status = TaskStatus.Faulted,
                        message = "Không tìm thấy đối tượng muốn xoá"
                    };
                }

                _context.Remove(existQuestion);
                await _context.SaveChangesAsync();
                return new Logger()
                {
                    status = TaskStatus.RanToCompletion,
                    message = "Xoá thành công",
                    data = existQuestion

                };
            }
            catch (Exception ex)
            {
                return new Logger()
                {
                    status = TaskStatus.Faulted,
                    message = ex.Message,
                };
            }
        }

        public async Task<Logger> GetAll()
        {
            try
            {
                var respone = await _context.QuestionBanks
                    .Include(_ => _.QB_Answers_MC)
                    .Include(_ => _.QB_Answer_Essay)
                    .Include(_ => _.User)
                    .Include(_ => _.Subject)
                    .ToListAsync();

                return new Logger()
                {
                    status = TaskStatus.RanToCompletion,
                    message = "Thành công",
                    listData = new List<object>() { respone }
                };
            }
            catch (Exception ex)
            {
                return new Logger()
                {
                    status = TaskStatus.Faulted,
                    message = ex.Message,
                };
            }
        }

        public async Task<Logger> GetById(int questionBanksId)
        {
            try
            {
                QuestionBanks? existQuestion = await _context.QuestionBanks
                    .Include(_ => _.QB_Answers_MC).Include(_ => _.QB_Answer_Essay)
                    .Include(_ => _.User).Include(_ => _.Subject)
                    .FirstOrDefaultAsync(x => x.Id == questionBanksId);

                if (existQuestion == null)
                {
                    return new Logger()
                    {
                        status = TaskStatus.Faulted,
                        message = "Không tìm thấy đối tượng cần tìm"
                    };
                }

                return new Logger()
                {
                    status = TaskStatus.RanToCompletion,
                    message = "Thành công",
                    data = existQuestion
                };
            }
            catch (Exception ex)
            {
                return new Logger()
                {
                    status = TaskStatus.Faulted,
                    message = ex.Message,
                };
            }
        }

        public async Task<Logger> Update(QuestionBanks questionBanks)
        {
            try
            {

                QuestionBanks? existQuestion = await _context.QuestionBanks
                    .Include(_ => _.QB_Answers_MC)
                    .Include(_ => _.QB_Answer_Essay).FirstOrDefaultAsync(x => x.Id == questionBanks.Id);

                if (existQuestion == null)
                {
                    return new Logger()
                    {
                        status = TaskStatus.Faulted,
                        message = "Không tìm thấy đối tượng cần cập nhật"
                    };
                }

                existQuestion.Format = questionBanks.Format;
                existQuestion.Content = questionBanks.Content;
                existQuestion.LastUpdated = DateTime.Now;
                existQuestion.Level = questionBanks.Level;
                existQuestion.TeacherCreatedId = questionBanks.TeacherCreatedId;
                existQuestion.SubjectId = questionBanks.SubjectId;
                existQuestion.QB_Answers_MC = questionBanks.QB_Answers_MC;
                existQuestion.QB_Answer_Essay = questionBanks.QB_Answer_Essay;

                await _context.SaveChangesAsync();

                return new Logger()
                {
                    status = TaskStatus.RanToCompletion,
                    message = "Cập nhật thành công",
                    data = existQuestion
                };
            }
            catch (Exception ex)
            {
                return new Logger()
                {
                    status = TaskStatus.Faulted,
                    message = ex.Message,
                };
            }
        }

        public async Task<ICollection<Question_Exam>> GetRandomQuestion(int easy, int medium, int hard, string subjectId)
        {
            var listQuestion = await _context.QuestionBanks.Include(_ => _.QB_Answers_MC)
                                                           .Where(_ => _.SubjectId == subjectId && _.Format == true)
                                                           .ToListAsync();

            List<QuestionBanks> easyQuestions = listQuestion.Where(_ => _.Level == Enums.Level.Easy).OrderBy(x => Guid.NewGuid()).Take(easy).ToList();

            List<QuestionBanks> mediumQuestions = listQuestion.Where(_ => _.Level == Enums.Level.Medium).OrderBy(x => Guid.NewGuid()).Take(medium).ToList();

            List<QuestionBanks> hardQuestions = listQuestion.Where(_ => _.Level == Enums.Level.Hard).OrderBy(x => Guid.NewGuid()).Take(hard).ToList();

            List<Question_Exam> question_Exams = new List<Question_Exam>();

            foreach (var item in easyQuestions)
            {
                Question_Exam question = new Question_Exam()
                {
                    QuestionId= item.Id,
                    QuestionBanks = item
                };

                question_Exams.Add(question);
            }

            foreach (var item in mediumQuestions)
            {
                Question_Exam question = new Question_Exam()
                {
                    QuestionId = item.Id,
                    QuestionBanks = item
                };

                question_Exams.Add(question);
            }

            foreach (var item in hardQuestions)
            {
                Question_Exam question = new Question_Exam()
                {
                    QuestionId = item.Id,
                    QuestionBanks = item
                };

                question_Exams.Add(question);
            }

            return question_Exams;
        }
    }
}
