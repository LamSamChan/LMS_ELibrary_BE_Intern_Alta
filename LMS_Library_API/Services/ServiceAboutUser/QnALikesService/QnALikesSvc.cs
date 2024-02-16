using LMS_Library_API.Context;
using LMS_Library_API.Models;
using LMS_Library_API.Models.AboutUser;
using LMS_Library_API.Models.RoleAccess;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace LMS_Library_API.Services.ServiceAboutUser.QnALikesService
{
    public class QnALikesSvc : IQnALikesSvc
    {
        private readonly DataContext _context;

        public QnALikesSvc(DataContext context)
        {
            _context = context;
        }

        public async Task<Logger> Create(QnALikes qnALike)
        {
            try
            {
                _context.QnALikes.Add(qnALike);
                await _context.SaveChangesAsync();
                return new Logger()
                {
                    status = TaskStatus.RanToCompletion,
                    message = "Thêm thành công",
                    data = qnALike
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

        public async Task<IEnumerable<QnALikes>> GetAll()
        {
            try
            {
                var respone = await _context.QnALikes.ToListAsync();
                return respone;
            }
            catch (Exception ex)
            {
                return new List<QnALikes>();
            }
        }

        public async Task<Logger> GetByUserId(string userId)
        {
            try
            {
                var data = await _context.QnALikes.FirstOrDefaultAsync(x => x.UserId == Guid.Parse(userId));

                if (data == null)
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
                    data = data
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

        public async Task<Logger> Update(QnALikes qnALike)
        {
            try
            {

                QnALikes existLikeList = await _context.QnALikes.FindAsync(qnALike.UserId);

                if (existLikeList == null)
                {
                    return new Logger()
                    {
                        status = TaskStatus.Faulted,
                        message = "Không tìm thấy đối tượng cần cập nhật"
                    };
                }

                existLikeList.QuestionsLikedID = qnALike.QuestionsLikedID;
                existLikeList.AnswersLikedID = qnALike.AnswersLikedID;

                await _context.SaveChangesAsync();

                return new Logger()
                {
                    status = TaskStatus.RanToCompletion,
                    message = "Cập nhật thành công",
                    data = existLikeList
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
