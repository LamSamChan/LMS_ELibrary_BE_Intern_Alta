using LMS_Library_API.Context;
using LMS_Library_API.Models;
using LMS_Library_API.Models.RoleAccess;
using Microsoft.EntityFrameworkCore;

namespace LMS_Library_API.Services.RoleAccess.PermissionsService
{
    public class PermissionsSvc : IPermissionsSvc
    {
        private readonly DataContext _context;

        public PermissionsSvc(DataContext context)
        {
            _context = context;
        }
        public async Task<Logger> Create(Permissions permissions)
        {
            try
            {
                _context.Permissions.Add(permissions);
                await _context.SaveChangesAsync();
                return new Logger()
                {
                    status = TaskStatus.RanToCompletion,
                    message = "Thêm thành công",
                    data = new Permissions() { Id = permissions.Id, Name = permissions.Name , Type = permissions.Type}
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

        public async Task<Logger> Delete(int permissionsId)
        {
            try
            {
                var existPermission = await _context.Permissions.FindAsync(permissionsId);

                if (existPermission == null)
                {
                    return new Logger()
                    {
                        status = TaskStatus.Faulted,
                        message = "Không tìm thấy đối tượng muốn xoá"
                    };
                }

                _context.Remove(existPermission);
                await _context.SaveChangesAsync();
                return new Logger()
                {
                    status = TaskStatus.RanToCompletion,
                    message = "Xoá thành công",
                    data = new Permissions() { Id = existPermission.Id, Name = existPermission.Name, Type = existPermission.Type }

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
                var respone = await _context.Permissions.ToListAsync();
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

        public async Task<Logger> GetById(int permissionsId)
        {
            try
            {
                Permissions existingPermissions = await _context.Permissions.FindAsync(permissionsId);

                if (existingPermissions == null)
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
                    data = new Permissions() { Id = existingPermissions.Id, Name = existingPermissions.Name, Type = existingPermissions.Type }
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

        public async Task<Logger> Update(Permissions permissions)
        {
            try
            {

                Permissions existPermissions = await _context.Permissions.FindAsync(permissions.Id);

                if (existPermissions == null)
                {
                    return new Logger()
                    {
                        status = TaskStatus.Faulted,
                        message = "Không tìm thấy đối tượng cần cập nhật"
                    };
                }

                existPermissions.Name = permissions.Name;
                existPermissions.Type = permissions.Type;

                await _context.SaveChangesAsync();

                return new Logger()
                {
                    status = TaskStatus.RanToCompletion,
                    message = "Cập nhật thành công",
                    data = new Permissions() { Id = existPermissions.Id, Name = existPermissions.Name, Type = existPermissions.Type }
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
