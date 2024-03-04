using LMS_Library_API.Context;
using LMS_Library_API.Models;
using LMS_Library_API.Models.Notification;
using LMS_Library_API.Models.StudentNotification;
using Microsoft.EntityFrameworkCore;

namespace LMS_Library_API.Services.ServiceAboutNotification.Stu_NotificationSettingService
{
    public class StuNotificationSettingSvc:IStuNotificationSettingSvc
    {
        private readonly DataContext _context;

        public StuNotificationSettingSvc(DataContext context)
        {
            _context = context;
        }

        public async Task<Logger> Create(StudentNotificationSetting setting)
        {
            try
            {
                _context.StudentNotificationSetting.Add(setting);
                await _context.SaveChangesAsync();
                return new Logger()
                {
                    status = TaskStatus.RanToCompletion,
                    message = "Thêm thành công",
                    data = setting
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

        public async Task<Logger> Delete(string studentId, int id)
        {
            try
            {
                var existSetting = await _context.StudentNotificationSetting.FirstOrDefaultAsync(_ => _.featuresId == id && _.studentId == Guid.Parse(studentId));

                if (existSetting == null)
                {
                    return new Logger()
                    {
                        status = TaskStatus.Faulted,
                        message = "Không tìm thấy đối tượng muốn xoá"
                    };
                }

                _context.Remove(existSetting);
                await _context.SaveChangesAsync();
                return new Logger()
                {
                    status = TaskStatus.RanToCompletion,
                    message = "Xoá thành công",
                    data = existSetting

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
                var respone = await _context.StudentNotificationSetting.ToListAsync();
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

        public async Task<Logger> GetById(string studentId, int id)
        {
            try
            {
                var existSetting = await _context.StudentNotificationSetting.Include(_ => _.StudentNotificationFeatures).FirstOrDefaultAsync(_ => _.featuresId == id && _.studentId == Guid.Parse(studentId));

                if (existSetting == null)
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
                    data = existSetting
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

        public async Task<Logger> GetByStudentId(string studentId)
        {
            try
            {
                var userSetting = await _context.StudentNotificationSetting.Include(_ => _.StudentNotificationFeatures).Where(_ => _.studentId == Guid.Parse(studentId)).ToListAsync();

                if (userSetting == null)
                {
                    return new Logger()
                    {
                        status = TaskStatus.Faulted,
                        message = "Không tìm thấy danh sách đối tượng cần tìm"
                    };
                }

                return new Logger()
                {
                    status = TaskStatus.RanToCompletion,
                    message = "Thành công",
                    listData = userSetting
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

        public async Task<Logger> Update(StudentNotificationSetting setting)
        {
            try
            {
                var existSetting = await _context.StudentNotificationSetting.FirstOrDefaultAsync(_ => _.featuresId == setting.featuresId && _.studentId == setting.studentId);

                if (existSetting == null)
                {
                    return new Logger()
                    {
                        status = TaskStatus.Faulted,
                        message = "Không tìm thấy đối tượng cần cập nhật"
                    };
                }

                existSetting.studentId = setting.studentId;
                existSetting.featuresId = setting.featuresId;
                existSetting.receiveNotification = setting.receiveNotification;

                await _context.SaveChangesAsync();

                return new Logger()
                {
                    status = TaskStatus.RanToCompletion,
                    message = "Cập nhật thành công",
                    data = existSetting
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
