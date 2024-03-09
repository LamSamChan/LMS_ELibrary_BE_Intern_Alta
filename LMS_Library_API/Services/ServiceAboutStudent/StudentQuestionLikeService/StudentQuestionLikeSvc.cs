using LMS_Library_API.Context;
using LMS_Library_API.Models;
using LMS_Library_API.Models.AboutStudent;
using Microsoft.EntityFrameworkCore;

namespace LMS_Library_API.Services.ServiceAboutStudent.StudentQuestionLikeService
{
    public class StudentQuestionLikeSvc: IStudentQuestionLikeSvc
    {
        private readonly DataContext _context;

        public StudentQuestionLikeSvc(DataContext context)
        {
            _context = context;
        }

        public async Task<Logger> Create(StudentQuestionLike questionLike)
        {
            try
            {
                _context.StudentQuestionLikes.Add(questionLike);
                await _context.SaveChangesAsync();
                return new Logger()
                {
                    status = TaskStatus.RanToCompletion,
                    message = "Thêm thành công",
                    data = questionLike
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

        public async Task<Logger> Delete(StudentQuestionLike existQuestionLike)
        {
            try
            {
                var exist = await _context.StudentQuestionLikes.FirstOrDefaultAsync(_ => _.StudentId == existQuestionLike.StudentId &&
                _.LessonId == existQuestionLike.LessonId &&
                _.LessonQuestionId == existQuestionLike.LessonQuestionId);

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
                var respone = await _context.StudentQuestionLikes.ToListAsync();

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
                var exist = await _context.StudentQuestionLikes
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
