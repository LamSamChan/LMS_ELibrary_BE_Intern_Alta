using LMS_Library_API.Context;
using LMS_Library_API.Models;
using LMS_Library_API.Models.AboutUser;
using Microsoft.EntityFrameworkCore;

namespace LMS_Library_API.Services.ServiceAboutUser.AnswerLikeService
{
    public class AnswerLikeSvc: IAnswerLikeSvc
    {
        private readonly DataContext _context;

        public AnswerLikeSvc(DataContext context)
        {
            _context = context;
        }

        public async Task<Logger> Create(AnswerLike answerLike)
        {
            try
            {
                _context.AnswerLikes.Add(answerLike);
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

        public async Task<Logger> Delete(AnswerLike ExistAnswerLike)
        {
            try
            {
                var exist = await _context.AnswerLikes.FirstOrDefaultAsync(_ => _.UserId == ExistAnswerLike.UserId &&
                _.LessonId == ExistAnswerLike.LessonId &&
                _.LessonAnswerId == ExistAnswerLike.LessonAnswerId);

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
                var respone = await _context.AnswerLikes.ToListAsync();

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
                var exist = await _context.AnswerLikes
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
