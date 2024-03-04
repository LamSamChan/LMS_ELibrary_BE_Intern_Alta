using LMS_Library_API.Context;
using LMS_Library_API.Models;
using LMS_Library_API.Models.Notification;
using LMS_Library_API.Models.StudentNotification;
using Microsoft.EntityFrameworkCore;

namespace LMS_Library_API.Services.ServiceAboutNotification.Stu_NotificationFeatuesService
{
    public class StuNotificationFeatuesSvc:IStuNotificationFeatuesSvc
    {
        private readonly DataContext _context;
        public StuNotificationFeatuesSvc(DataContext context)
        {
            _context = context;
        }

        public async Task<Logger> Create(StudentNotificationFeatures notificationFeature)
        {
            try
            {
                _context.StudentNotificationFeatures.Add(notificationFeature);
                await _context.SaveChangesAsync();
                return new Logger()
                {
                    status = TaskStatus.RanToCompletion,
                    message = "Thêm thành công",
                    data = notificationFeature
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

        public async Task<Logger> Delete(int id)
        {
            try
            {
                var existFeature = await _context.StudentNotificationFeatures.FindAsync(id);

                if (existFeature == null)
                {
                    return new Logger()
                    {
                        status = TaskStatus.Faulted,
                        message = "Không tìm thấy đối tượng muốn xoá"
                    };
                }

                _context.Remove(existFeature);
                await _context.SaveChangesAsync();
                return new Logger()
                {
                    status = TaskStatus.RanToCompletion,
                    message = "Xoá thành công",
                    data = existFeature

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
                var respone = await _context.StudentNotificationFeatures.ToListAsync();
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

        public async Task<Logger> GetById(int id)
        {
            try
            {
                StudentNotificationFeatures existFeature = await _context.StudentNotificationFeatures.FindAsync(id);

                if (existFeature == null)
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
                    data = existFeature
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

        public async Task<Logger> Update(StudentNotificationFeatures notificationFeature)
        {
            try
            {

                StudentNotificationFeatures existFeature = await _context.StudentNotificationFeatures.FindAsync(notificationFeature.Id);

                if (existFeature == null)
                {
                    return new Logger()
                    {
                        status = TaskStatus.Faulted,
                        message = "Không tìm thấy đối tượng cần cập nhật"
                    };
                }

                existFeature.Id = notificationFeature.Id;
                existFeature.FeatureType = notificationFeature.FeatureType;
                existFeature.Type = notificationFeature.Type;

                await _context.SaveChangesAsync();

                return new Logger()
                {
                    status = TaskStatus.RanToCompletion,
                    message = "Cập nhật thành công",
                    data = existFeature
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
