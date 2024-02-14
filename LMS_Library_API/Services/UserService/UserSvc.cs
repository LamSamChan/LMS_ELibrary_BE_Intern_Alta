using LMS_Library_API.Context;
using LMS_Library_API.Helpers;
using LMS_Library_API.Models;
using LMS_Library_API.Models.RoleAccess;
using Microsoft.EntityFrameworkCore;

namespace LMS_Library_API.Services.UserService
{
    public class UserSvc : IUserSvc
    {

        private readonly DataContext _context;
        private readonly EncodeHelper _encodeHelper;

        public UserSvc(DataContext context, EncodeHelper encodeHelper)
        {
            _context = context;
            _encodeHelper = encodeHelper;
        }

        public async Task<Logger> Create(User user)
        {
            try
            {
                user.Password = _encodeHelper.Encode(user.Password);
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
                return new Logger()
                {
                    status = TaskStatus.RanToCompletion,
                    message = "Thêm thành công",
                    data = user
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
                var respone = await _context.Users.ToListAsync();
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

        public async Task<Logger> GetById(string userId)
        {
            try
            {
                User existUser = await _context.Users.Include(_ => _.ExamRecentViews).Include(_ => _.TeacherClasses).Include(_ => _.Role).Include(_ => _.Department)
                    .FirstOrDefaultAsync(x => x.Id == Guid.Parse(userId));

                if (existUser == null)
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
                    data = existUser
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

        public async Task<Logger> Search(string query)
        {
            try
            {
                var respone = await _context.Users.Where(d => d.FullName.ToUpper().Contains(query.ToUpper()) || d.Id.ToString().ToUpper().Contains(query.ToUpper()) || d.Email.ToUpper().Contains(query.ToUpper())
                || d.PhoneNumber.ToUpper().Contains(query.ToUpper()))
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

        public async Task<Logger> Update(User user)
        {
            throw new NotImplementedException();
        }
    }
}
