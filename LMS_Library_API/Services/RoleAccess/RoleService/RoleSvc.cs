using LMS_Library_API.Context;
using LMS_Library_API.Models;
using LMS_Library_API.Models.RoleAccess;
using Microsoft.EntityFrameworkCore;
using System.Security;

namespace LMS_Library_API.Services.RoleAccess.RoleService
{
    public class RoleSvc : IRoleSvc
    {
        private readonly DataContext _context;

        public RoleSvc(DataContext context)
        {
            _context = context;
        }

        public async Task<Logger> Create(Role role)
        {
            try
            {
                _context.Role.Add(role);
                await _context.SaveChangesAsync();
                return new Logger()
                {
                    status = TaskStatus.RanToCompletion,
                    message = "Thêm thành công",
                    data = role
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

        public async Task<Logger> Delete(int roleId)
        {
            try
            {
                var existRole = await _context.Role.Include(_ => _.Role_Permissions).FirstOrDefaultAsync(_ => _.Id == roleId);

                if (existRole == null)
                {
                    return new Logger()
                    {
                        status = TaskStatus.Faulted,
                        message = "Không tìm thấy đối tượng muốn xoá"
                    };
                }

                _context.Remove(existRole);
                await _context.SaveChangesAsync();
                return new Logger()
                {
                    status = TaskStatus.RanToCompletion,
                    message = "Xoá thành công",
                    data = existRole

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
                var respone = await _context.Role.Include(_ => _.Role_Permissions).ThenInclude(_ => _.Permissions).ToListAsync();
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

        public async Task<Logger> GetById(int roleId)
        {
            try
            {
                Role existRole = await _context.Role.Include(_ => _.Role_Permissions).ThenInclude(_ => _.Permissions).FirstOrDefaultAsync(x => x.Id == roleId);

                if (existRole == null)
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
                    data = existRole
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

        public async Task<Logger> Update(Role role)
        {
            try
            {

                Role existRole = await _context.Role.Include(_ => _.Role_Permissions).FirstOrDefaultAsync(x => x.Id == role.Id);

                if (existRole == null)
                {
                    return new Logger()
                    {
                        status = TaskStatus.Faulted,
                        message = "Không tìm thấy đối tượng cần cập nhật"
                    };
                }

                existRole.Name = role.Name;
                existRole.Description = role.Description;
                existRole.DateUpdated = DateTime.Now;
                existRole.Role_Permissions = role.Role_Permissions;

                await _context.SaveChangesAsync();

                return new Logger()
                {
                    status = TaskStatus.RanToCompletion,
                    message = "Cập nhật thành công",
                    data = existRole
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
