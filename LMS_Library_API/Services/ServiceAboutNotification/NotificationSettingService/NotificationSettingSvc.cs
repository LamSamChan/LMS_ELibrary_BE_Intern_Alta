using LMS_Library_API.Context;
using LMS_Library_API.Models;
using LMS_Library_API.Models.Notification;
using Microsoft.EntityFrameworkCore;

namespace LMS_Library_API.Services.ServiceAboutNotification.NotificationSettingService
{
    public class NotificationSettingSvc:INotificationSettingSvc
    {
        private readonly DataContext _context;

        public NotificationSettingSvc(DataContext context)
        {
            _context = context;
        }

        public async Task<Logger> Create(NotificationSetting setting)
        {
            try
            {
                _context.NotificationSetting.Add(setting);
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

        public async Task<Logger> Delete(string userId, int id)
        {
            try
            {
                var existSetting = await _context.NotificationSetting.FirstOrDefaultAsync(_ => _.FeaturesId == id && _.UserId == Guid.Parse(userId));

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
                var respone = await _context.NotificationSetting.ToListAsync();
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

        public async Task<Logger> GetById(string userId, int id)
        {
            try
            {
                var existSetting = await _context.NotificationSetting.Include(_ => _.NotificationFeatures).FirstOrDefaultAsync(_ => _.FeaturesId == id && _.UserId == Guid.Parse(userId));

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

        public async Task<Logger> GetByUserId(string userId)
        {
            try
            {
                var userSetting = await _context.NotificationSetting.Include(_ => _.NotificationFeatures).Where(_ => _.UserId == Guid.Parse(userId)).ToListAsync();

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

        public async Task<Logger> Update(NotificationSetting setting)
        {
            try
            {
                var existSetting = await _context.NotificationSetting.FirstOrDefaultAsync(_ => _.FeaturesId == setting.FeaturesId && _.UserId == setting.UserId);

                if (existSetting == null)
                {
                    return new Logger()
                    {
                        status = TaskStatus.Faulted,
                        message = "Không tìm thấy đối tượng cần cập nhật"
                    };
                }

                existSetting.UserId = setting.UserId;
                existSetting.FeaturesId = setting.FeaturesId;
                existSetting.ReceiveNotification = setting.ReceiveNotification;

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
