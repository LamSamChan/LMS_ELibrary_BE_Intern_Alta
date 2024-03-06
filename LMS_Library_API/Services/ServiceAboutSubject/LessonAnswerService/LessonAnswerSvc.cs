using LMS_Library_API.Context;
using LMS_Library_API.Models;
using LMS_Library_API.Models.AboutSubject;
using Microsoft.EntityFrameworkCore;

namespace LMS_Library_API.Services.ServiceAboutSubject.LessonAnswerService
{
    public class LessonAnswerSvc : ILessonAnswerSvc
    {
        private readonly DataContext _context;

        public LessonAnswerSvc(DataContext context)
        {
            _context = context;
        }

        public async Task<Logger> Create(LessonAnswer lessonAnswer)
        {
            try
            {
                _context.LessonAnswers.Add(lessonAnswer);
                await _context.SaveChangesAsync();
                return new Logger()
                {
                    status = TaskStatus.RanToCompletion,
                    message = "Thêm thành công",
                    data = lessonAnswer
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

        public async Task<Logger> Delete(int id)
        {
            try
            {
                var existAnswer = await _context.LessonAnswers.FindAsync(id);

                if (existAnswer == null)
                {
                    return new Logger()
                    {
                        status = TaskStatus.Faulted,
                        message = "Không tìm thấy đối tượng muốn xoá"
                    };
                }

                _context.Remove(existAnswer);
                await _context.SaveChangesAsync();
                return new Logger()
                {
                    status = TaskStatus.RanToCompletion,
                    message = "Xoá thành công",
                    data = existAnswer

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
                var respone = await _context.LessonAnswers.ToListAsync();
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

        public async Task<Logger> GetById(int id)
        {
            try
            {
                var existAnswer = await _context.LessonAnswers.Include(_ => _.LessonQuestion).FirstOrDefaultAsync(_ => _.Id == id);

                if (existAnswer == null)
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
                    data = existAnswer
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

        public async Task<Logger> GetByQuestionId(int questionId)
        {
            try
            {
                var existAnswer = await _context.LessonAnswers.Where(_ => _.lessonQuestionId == questionId).ToListAsync();

                if (existAnswer == null)
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
                    data = existAnswer
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

        public async Task<Logger> Update(LessonAnswer lessonAnswer)
        {
            try
            {
                var existAnswer = await _context.LessonAnswers.FindAsync(lessonAnswer.Id);

                if (existAnswer == null)
                {
                    return new Logger()
                    {
                        status = TaskStatus.Faulted,
                        message = "Không tìm thấy đối tượng cần cập nhật"
                    };
                }

                existAnswer.Id = lessonAnswer.Id;
                existAnswer.content = lessonAnswer.content;
                existAnswer.createdAt = lessonAnswer.createdAt;
                existAnswer.likesCounter = lessonAnswer.likesCounter;
                existAnswer.isTeacher = lessonAnswer.isTeacher;
                existAnswer.lessonQuestionId = lessonAnswer.lessonQuestionId;
                existAnswer.teacherId = lessonAnswer.teacherId;
                existAnswer.studentId = lessonAnswer.studentId;

                await _context.SaveChangesAsync();

                return new Logger()
                {
                    status = TaskStatus.RanToCompletion,
                    message = "Cập nhật thành công",
                    data = existAnswer
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
