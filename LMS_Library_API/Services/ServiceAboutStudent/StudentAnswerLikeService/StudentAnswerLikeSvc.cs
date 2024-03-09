using LMS_Library_API.Context;
using LMS_Library_API.Models;
using LMS_Library_API.Models.AboutStudent;
using LMS_Library_API.Models.AboutUser;
using Microsoft.EntityFrameworkCore;

namespace LMS_Library_API.Services.ServiceAboutStudent.StudentAnswerLikeService
{
    public class StudentAnswerLikeSvc: IStudentAnswerLikeSvc
    {
        private readonly DataContext _context;

        public StudentAnswerLikeSvc(DataContext context)
        {
            _context = context;
        }

        public async Task<Logger> Create(StudentAnswerLike answerLike)
        {
            try
            {
                _context.StudentAnswerLikes.Add(answerLike);
                await _context.SaveChangesAsync();
                return new Logger()
                {
                    status = TaskStatus.RanToCompletion,
                    message = "Thêm thành công",
                    data = answerLike
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

        public async Task<Logger> Delete(StudentAnswerLike existAnswerLike)
        {
            try
            {
                var exist = await _context.StudentAnswerLikes.FirstOrDefaultAsync(_ => _.StudentId == existAnswerLike.StudentId &&
                _.LessonId == existAnswerLike.LessonId &&
                _.LessonAnswerId == existAnswerLike.LessonAnswerId);

                if (exist == null)
                {
                    return new Logger()
                    {
                        status = TaskStatus.Faulted,
                        message = "Không tìm thấy đối tượng muốn xoá"
                    };
                }

                _context.Remove(exist);
                await _context.SaveChangesAsync();
                return new Logger()
                {
                    status = TaskStatus.RanToCompletion,
                    message = "Xoá thành công",
                    data = exist

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
                var respone = await _context.StudentAnswerLikes.ToListAsync();

                return new Logger()
                {
                    status = TaskStatus.RanToCompletion,
                    message = "Thành công",
                    listData = respone
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

        public async Task<Logger> GetByStudentLesson(string studentId, int lessonId)
        {
            try
            {
                var exist = await _context.StudentAnswerLikes
                    .Where(x => x.StudentId == Guid.Parse(studentId) && x.LessonId == lessonId).ToListAsync();

                if (exist == null)
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
                    data = exist
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
