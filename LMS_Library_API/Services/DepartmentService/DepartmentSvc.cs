using LMS_Library_API.Context;
using LMS_Library_API.Models;
using LMS_Library_API.Models.RoleAccess;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Net.WebSockets;

namespace LMS_Library_API.Services.DepartmentService
{
    public class DepartmentSvc : IDepartmentSvc
    {
        private readonly DataContext _context;

        public DepartmentSvc(DataContext context)
        {
            _context = context;
        }
        public async Task<Logger> Create(Department department)
        {
            try
            {
                _context.Departments.Add(department);
                await _context.SaveChangesAsync();
                return new Logger() {
                    status = TaskStatus.RanToCompletion,
                    message = "Thêm thành công",
                    data = new Department() { Id = department.Id, Name = department.Name }
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

        public async Task<Logger> Delete(string departmentId)
        {
            try
            {
                var existDepartment = await _context.Departments.FindAsync(departmentId);
                
                if (existDepartment == null)
                {
                    return new Logger()
                    {
                        status = TaskStatus.Faulted,
                        message = "Không tìm thấy đối tượng muốn xoá"
                    };
                }

                _context.Remove(existDepartment);
                await _context.SaveChangesAsync();
                return new Logger()
                {
                    status = TaskStatus.RanToCompletion,
                    message = "Xoá thành công",
                    data = new Department() { Id = existDepartment.Id, Name = existDepartment.Name }

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
                var respone = await _context.Departments.ToListAsync();
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

        public async Task<Logger> GetById(string departmentId)
        {
            try
            {
                Department? existingDepartment = await _context.Departments.FindAsync(departmentId.ToUpper());

                if (existingDepartment == null)
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
                    data = new Department() { Id = existingDepartment.Id, Name = existingDepartment.Name }
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

        public async Task<IEnumerable<Department>> GetCheck()
        {
            try
            {
                var respone = await _context.Departments.ToListAsync();
                return respone;
            }
            catch (Exception)
            {
                return new List<Department>();
            }
        }

        public async Task<Logger> Search(string query)
        {
            try
            {
                var respone = await _context.Departments.Where(d => d.Name.ToUpper().Contains(query) || d.Id.ToUpper().Contains(query)).ToListAsync();
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

        public async Task<Logger> Update(Department department)
        {
            try
            {

                Department? existingDepartment = await _context.Departments.FindAsync(department.Id.ToUpper());

                if (existingDepartment == null)
                {
                    return new Logger()
                    {
                        status = TaskStatus.Faulted,
                        message = "Không tìm thấy đối tượng cần cập nhật"
                    };
                }

                existingDepartment.Name = department.Name;

                await _context.SaveChangesAsync();

                return new Logger()
                {
                    status = TaskStatus.RanToCompletion,
                    message = "Cập nhật thành công",
                    data = new Department() { Id = existingDepartment.Id, Name = existingDepartment.Name }
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
