using LMS_Library_API.Context;
using LMS_Library_API.Models;
using LMS_Library_API.Models.Exams;
using LMS_Library_API.Services.ServiceAboutExam.QuestionBankService;
using Microsoft.EntityFrameworkCore;

namespace LMS_Library_API.Services.ServiceAboutExam.Question_ExamService
{
    public class QuestionExamSvc:IQuestionExamSvc
    {
        private readonly DataContext _context;

        public QuestionExamSvc(DataContext context)
        {
            _context = context; 
        }

        public async Task<Logger> CheckFormat(string examId, int quesionId)
        {
            try
            {
                var examFormat = await _context.Exams.FindAsync(examId);
                var questionFormat = await _context.QuestionBanks.FindAsync(quesionId);

                if (examFormat == null || questionFormat == null)
                {
                    return new Logger()
                    {
                        status = TaskStatus.Faulted,
                        message = "Để thi hoặc câu hỏi không tồn tại",
                    };
                }

                if (examFormat.Format == examFormat.Format)
                {
                    return new Logger()
                    {
                        status = TaskStatus.RanToCompletion,
                    };
                }
                else
                {
                    return new Logger()
                    {
                        status = TaskStatus.Faulted,
                        message = "Để thi và câu hỏi không chung hình thức",

                    };
                }

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

        public async Task<Logger> Create(Question_Exam questionExam)
        {
            try
            {
                _context.Question_Exam.Add(questionExam);
                await _context.SaveChangesAsync();
                return new Logger()
                {
                    status = TaskStatus.RanToCompletion,
                    message = "Thêm thành công",
                    data = questionExam
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

        public async Task<Logger> Delete(string examId, int quesionId)
        {
            try
            {
                var existQuestion = await _context.Question_Exam
                    .FirstOrDefaultAsync(_ => _.ExamId == examId && _.QuestionId == quesionId);

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
                var respone = await _context.Question_Exam.ToListAsync();

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

        public async Task<Logger> GetById(string examId, int quesionId)
        {
            try
            {
                var existQuestion = await _context.Question_Exam
                    .FirstOrDefaultAsync(x => x.ExamId == examId && x.QuestionId == quesionId);

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

        public async Task<Logger> Update(Question_Exam questionExam)
        {
            try
            {

                var existQuestion = await _context.Question_Exam
                    .FirstOrDefaultAsync(x => x.ExamId == questionExam.ExamId && x.QuestionId == questionExam.QuestionId);

                if (existQuestion == null)
                {
                    return new Logger()
                    {
                        status = TaskStatus.Faulted,
                        message = "Không tìm thấy đối tượng cần cập nhật"
                    };
                }

                existQuestion.ExamId = questionExam.ExamId;
                existQuestion.QuestionId = questionExam.QuestionId;

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
    }
}
