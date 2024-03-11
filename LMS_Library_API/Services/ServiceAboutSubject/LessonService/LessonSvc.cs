using LMS_Library_API.Context;
using LMS_Library_API.Models;
using LMS_Library_API.Models.AboutSubject;
using Microsoft.EntityFrameworkCore;

namespace LMS_Library_API.Services.ServiceAboutSubject.LessonService
{
    public class LessonSvc:ILessonSvc
    {
        private readonly DataContext _context;

        public LessonSvc( DataContext context)
        {
            _context = context;
        }

        public async Task<Logger> Create(Lesson lesson)
        {
            try
            {
                lesson.censorId = null;
                _context.Lessons.Add(lesson);
                await _context.SaveChangesAsync();
                return new Logger()
                {
                    status = TaskStatus.RanToCompletion,
                    message = "Thêm thành công",
                    data = lesson
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

        public async Task<Logger> Delete(int lessonId)
        {
            try
            {
                var existLesson = await _context.Lessons.FindAsync(lessonId);

                if (existLesson == null)
                {
                    return new Logger()
                    {
                        status = TaskStatus.Faulted,
                        message = "Không tìm thấy đối tượng muốn xoá"
                    };
                }

                _context.Remove(existLesson);
                await _context.SaveChangesAsync();
                return new Logger()
                {
                    status = TaskStatus.RanToCompletion,
                    message = "Xoá thành công",
                    data = existLesson

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
                var respone = await _context.Lessons
                    .Include(_ => _.TeacherCreated)
                    .Include(_ => _.Censor)
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

        public async Task<Logger> GetById(int lessonId)
        {
            try
            {
                var existLesson = await _context.Lessons
                    .Include(_ => _.LessonQuestions)
                        .ThenInclude(_ => _.Student)
                    .Include(_ => _.LessonQuestions)
                        .ThenInclude(_ => _.User)
                    .Include(_ => _.LessonQuestions)
                        .ThenInclude(_ => _.LessonAnswers)
                            .ThenInclude(_ => _.Student)
                        .ThenInclude(_ => _.LessonAnswers)
                            .ThenInclude(_ => _.User)
                    .Include(_ => _.Documents)
                    .FirstOrDefaultAsync(_ => _.Id == lessonId);

                if (existLesson == null)
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
                    data = existLesson
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

        public async Task<Logger> Update(Lesson lesson)
        {
            try
            {

                Lesson? existLesson = await _context.Lessons.FindAsync(lesson.Id);

                if (existLesson == null)
                {
                    return new Logger()
                    {
                        status = TaskStatus.Faulted,
                        message = "Không tìm thấy đối tượng cần cập nhật"
                    };
                }

                existLesson.title = lesson.title;
                existLesson.dateSubmited = DateTime.Now;
                existLesson.isHidden = lesson.isHidden;
                existLesson.numericalOrder = lesson.numericalOrder;
                existLesson.status = lesson.status;
                existLesson.note = lesson.note;
                existLesson.partId = lesson.partId;
                existLesson.censorId = lesson.censorId;
                existLesson.teacherCreatedId = lesson.teacherCreatedId;

                await _context.SaveChangesAsync();

                return new Logger()
                {
                    status = TaskStatus.RanToCompletion,
                    message = "Cập nhật thành công",
                    data = existLesson
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
