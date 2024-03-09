using LMS_Library_API.Context;
using LMS_Library_API.Helpers;
using LMS_Library_API.Models;
using LMS_Library_API.Models.RoleAccess;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace LMS_Library_API.Services.UserService
{
    public class UserSvc : IUserSvc
    {

        private readonly DataContext _context;
        private readonly IEncodeHelper _encodeHelper;

        public UserSvc(DataContext context, IEncodeHelper encodeHelper)
        {
            _context = context;
            _encodeHelper = encodeHelper;
        }

        public async Task<Logger> Create(User user)
        {
            try
            {
                var userList = await _context.Users.ToListAsync();

                foreach (var item in userList)
                {
                    if (string.Equals(user.Email, item.Email, StringComparison.OrdinalIgnoreCase))
                    {
                        return new Logger()
                        {
                            status = TaskStatus.Faulted,
                            message = "Email đã tồn tại",
                            data = user
                        };
                    }

                    if (string.Equals(user.PhoneNumber.Substring(user.PhoneNumber.Length - 9), item.PhoneNumber.Substring(item.PhoneNumber.Length - 9)))
                    {
                        return new Logger()
                        {
                            status = TaskStatus.Faulted,
                            message = "Số điện thoại đã tồn tại",
                            data = user
                        };
                    }
                }

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
                var respone = await _context.Users.Include(_ => _.Role)
                    .Include(_ => _.Department)
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

        public async Task<Logger> GetById(string userId)
        {
            try
            {
                User? existUser = await _context.Users.Include(_ => _.Role).ThenInclude(_ => _.Role_Permissions).ThenInclude(_ => _.Permissions)
                    .Include(_ => _.Department)
                    .Include(_ => _.ExamRecentViews)
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
                var respone = await _context.Users.Where(d => d.FullName.ToUpper().Contains(query) || d.Id.ToString().ToUpper().Contains(query) || d.Email.ToUpper().Contains(query)
                || d.PhoneNumber.ToUpper().Contains(query))
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
            try
            {

                User? existUser = await _context.Users.FindAsync(user.Id);

                if (existUser == null)
                {
                    return new Logger()
                    {
                        status = TaskStatus.Faulted,
                        message = "Không tìm thấy đối tượng cần cập nhật"
                    };
                }


                if (user.Avartar == null)
                {
                    existUser.Avartar = existUser.Avartar;
                }
                else
                {
                    existUser.Avartar = user.Avartar;
                }

                existUser.FullName = user.FullName;
                existUser.Email = user.Email;
                existUser.DateOfBirth = user.DateOfBirth;
                existUser.PhoneNumber = user.PhoneNumber;
                existUser.Address = user.Address;
                existUser.Gender = user.Gender;
                existUser.Password = user.Password;
                existUser.RoleId = user.RoleId;
                existUser.Role = user.Role;
                existUser.DepartmentId = user.DepartmentId;
                existUser.Department = user.Department;

                await _context.SaveChangesAsync();

                return new Logger()
                {
                    status = TaskStatus.RanToCompletion,
                    message = "Cập nhật thành công",
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
    }
}
