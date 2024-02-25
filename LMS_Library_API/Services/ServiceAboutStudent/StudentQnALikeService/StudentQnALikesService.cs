using LMS_Library_API.Context;
using LMS_Library_API.Models;
using LMS_Library_API.Models.AboutStudent;
using LMS_Library_API.Models.AboutUser;
using Microsoft.EntityFrameworkCore;

namespace LMS_Library_API.Services.ServiceAboutStudent.StudentQnALikeService
{
    public class StudentStudentQnALikesService:IStudentQnALikesService
    {
        private readonly DataContext _context;

        public StudentStudentQnALikesService(DataContext context)
        {
            _context = context;
        }

        public async Task<Logger> Create(StudentQnALikes qnALike)
        {
            try
            {
                _context.StudentQnALikes.Add(qnALike);
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

        public async Task<IEnumerable<StudentQnALikes>> GetAll()
        {
            try
            {
                var respone = await _context.StudentQnALikes.ToListAsync();
                return respone;
            }
            catch (Exception ex)
            {
                return new List<StudentQnALikes>();
            }
        }

        public async Task<Logger> GetByStudentId(string studentId)
        {
            try
            {
                var data = await _context.StudentQnALikes.FirstOrDefaultAsync(x => x.studentId == Guid.Parse(studentId));

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

        public async Task<Logger> Update(StudentQnALikes qnALike)
        {
            try
            {

                StudentQnALikes existLikeList = await _context.StudentQnALikes.FindAsync(qnALike.studentId);

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
