using LMS_Library_API.Context;
using LMS_Library_API.Helpers;
using LMS_Library_API.Models;
using Microsoft.EntityFrameworkCore;

namespace LMS_Library_API.Services.SystemInfomationService
{
    public class SystemInfomationSvc : ISystemInfomationSvc
    {

        private readonly DataContext _context;

        public SystemInfomationSvc(DataContext context)
        {
            _context = context;
        }

        public async Task<Logger> Create(SystemInfomation infomation)
        {
            try
            {
                var userList = await _context.SystemInfomation.ToListAsync();
                if (userList.Count > 0)
                {
                   return new Logger()
                   {
                      status = TaskStatus.Faulted,
                      message = "Chỉ có thể tồn tại 1 dữ liệu thông tin hệ thống"
                   };
                }         

                _context.SystemInfomation.Add(infomation);
                await _context.SaveChangesAsync();
                return new Logger()
                {
                    status = TaskStatus.RanToCompletion,
                    message = "Thêm thành công",
                    data = infomation
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

        public async Task<Logger> Delete(string id)
        {
            try
            {
                var existInfo = await _context.SystemInfomation.FindAsync(Guid.Parse(id));

                if (existInfo == null)
                {
                    return new Logger()
                    {
                        status = TaskStatus.Faulted,
                        message = "Không tìm thấy đối tượng muốn xoá"
                    };
                }

                _context.Remove(existInfo);
                await _context.SaveChangesAsync();
                return new Logger()
                {
                    status = TaskStatus.RanToCompletion,
                    message = "Xoá thành công",
                    data = existInfo

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

        public async Task<Logger> GetInfo()
        {
            try
            {
                var respone = await _context.SystemInfomation.FirstAsync();
                return new Logger()
                {
                    status = TaskStatus.RanToCompletion,
                    message = "Thành công",
                    data =  respone
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

        public async Task<Logger> Update(SystemInfomation infomation)
        {
            try
            {

                SystemInfomation existInfomation = await _context.SystemInfomation.FindAsync(infomation.Id);

                if (existInfomation == null)
                {
                    return new Logger()
                    {
                        status = TaskStatus.Faulted,
                        message = "Không tìm thấy đối tượng cần cập nhật"
                    };
                }


                if (infomation.SchoolLogo == null)
                {
                    existInfomation.SchoolLogo = existInfomation.SchoolLogo;
                }
                else
                {
                    existInfomation.SchoolLogo = infomation.SchoolLogo;
                }

                existInfomation.SchoolId = infomation.SchoolId;
                existInfomation.Name = infomation.Name;
                existInfomation.SchoolWebsite = infomation.SchoolWebsite;
                existInfomation.SchoolType = infomation.SchoolType;
                existInfomation.LibrarySystemName = infomation.LibrarySystemName;
                existInfomation.LMSWebsite = infomation.LMSWebsite;
                existInfomation.PhoneNumber = infomation.PhoneNumber;
                existInfomation.Email = infomation.Email;
                existInfomation.Language = infomation.Language;
                existInfomation.AcademicYear = infomation.AcademicYear;
                existInfomation.Principals = infomation.Principals;

                await _context.SaveChangesAsync();

                return new Logger()
                {
                    status = TaskStatus.RanToCompletion,
                    message = "Cập nhật thành công",
                    data = existInfomation
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
