using LMS_Library_API.Context;
using LMS_Library_API.Models;
using LMS_Library_API.Models.AboutSubject;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace LMS_Library_API.Services.ServiceAboutSubject.LessonQuestionService
{
    public class LessonQuestionSvc: ILessonQuestionSvc
    {
        private readonly DataContext _context;

        public LessonQuestionSvc(DataContext context)
        {
            _context = context;
        }

        public async Task<Logger> Create(LessonQuestion lessonQuestion)
        {
            try
            {
                _context.LessonQuestions.Add(lessonQuestion);
                await _context.SaveChangesAsync();
                return new Logger()
                {
                    status = TaskStatus.RanToCompletion,
                    message = "Thêm thành công",
                    data = lessonQuestion
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
                var existQuestion = await _context.LessonQuestions.FindAsync(id);

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
                var respone = await _context.LessonQuestions.ToListAsync();
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
                var existQuestion = await _context.LessonQuestions.Include(_ => _.LessonAnswers).FirstOrDefaultAsync(_ => _.Id == id);

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

        public async Task<Logger> GetByLessonId(int lessonId)
        {
            try
            {
                var existQuestion = await _context.LessonQuestions.Include(_ => _.LessonAnswers).Where(_ => _.lessonId == lessonId).ToListAsync();

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

        public async Task<Logger> Update(LessonQuestion lessonQuestion)
        {
            try
            {
                var existQuestion = await _context.LessonQuestions.FindAsync(lessonQuestion.Id);

                if (existQuestion == null)
                {
                    return new Logger()
                    {
                        status = TaskStatus.Faulted,
                        message = "Không tìm thấy đối tượng cần cập nhật"
                    };
                }

                existQuestion.Id = lessonQuestion.Id;
                existQuestion.content = lessonQuestion.content;
                existQuestion.createdAt = lessonQuestion.createdAt;
                existQuestion.likesCounter = lessonQuestion.likesCounter;
                existQuestion.isTeacher = lessonQuestion.isTeacher;
                existQuestion.lessonId = lessonQuestion.lessonId;
                existQuestion.teacherId = lessonQuestion.teacherId;
                existQuestion.studentId = lessonQuestion.studentId;

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
