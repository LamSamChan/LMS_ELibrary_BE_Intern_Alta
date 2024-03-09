using LMS_Library_API.Context;
using LMS_Library_API.Models;
using LMS_Library_API.Models.AboutUser;
using Microsoft.EntityFrameworkCore;

namespace LMS_Library_API.Services.ServiceAboutUser.QuestionLikeService
{
    public class QuestionLikeSvc: IQuestionLikeSvc
    {
        private readonly DataContext _context;

        public QuestionLikeSvc(DataContext context)
        {
            _context = context;
        }

        public async Task<Logger> Create(QuestionLike questionLike)
        {
            try
            {
                _context.QuestionLikes.Add(questionLike);
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

        public async Task<Logger> Delete(QuestionLike existQuestionLike)
        {
            try
            {
                var exist = await _context.QuestionLikes.FirstOrDefaultAsync(_ => _.UserId == existQuestionLike.UserId &&
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
                var respone = await _context.QuestionLikes.ToListAsync();

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

        public async Task<Logger> GetByUserLesson(string userId, int lessonId)
        {
            try
            {
                var exist = await _context.QuestionLikes
                    .Where(x => x.UserId == Guid.Parse(userId) && x.LessonId == lessonId).ToListAsync();

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
