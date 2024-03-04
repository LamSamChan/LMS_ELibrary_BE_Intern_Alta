using LMS_Library_API.Context;
using LMS_Library_API.Models;
using LMS_Library_API.Models.Notification;
using Microsoft.EntityFrameworkCore;

namespace LMS_Library_API.Services.ServiceAboutNotification.NotificationService
{
    public class NotificationSvc: INotificationSvc
    {
        private readonly DataContext _context;
        public NotificationSvc(DataContext context)
        {
            _context = context;
        }

        public async Task<Logger> Create(Notification notification)
        {
            try
            {
                _context.Notifications.Add(notification);
                await _context.SaveChangesAsync();
                return new Logger()
                {
                    status = TaskStatus.RanToCompletion,
                    message = "Thêm thành công",
                    data = notification
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

        public async Task<Logger> Delete(int notificationId)
        {
            try
            {
                var existNotification = await _context.Notifications.FindAsync(notificationId);

                if (existNotification == null)
                {
                    return new Logger()
                    {
                        status = TaskStatus.Faulted,
                        message = "Không tìm thấy đối tượng muốn xoá"
                    };
                }

                _context.Remove(existNotification);
                await _context.SaveChangesAsync();
                return new Logger()
                {
                    status = TaskStatus.RanToCompletion,
                    message = "Xoá thành công",
                    data = existNotification
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
                var respone = await _context.Notifications
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

        public async Task<Logger> GetById(int notificationId)
        {
            try
            {
                Notification existNotification = await _context.Notifications.Include(_ => _.Sender)
                    .FirstOrDefaultAsync(_ => _.Id == notificationId);
                    

                if (existNotification == null)
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
                    data = existNotification
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

        public async Task<Logger> GetByTeacherRecipientId(string userId)
        {
            try
            {
                var listNotification = await _context.Notifications.Where(_ => _.RecipientId == Guid.Parse(userId))
                    .Include(_ => _.StudentSender)
                    .Include(_ => _.Sender)
                    .ToListAsync();

                if (listNotification == null)
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
                    listData = listNotification
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

        public async Task<Logger> SearchTeacherRecipient(string userId, string query)
        {
            try
            {
                var listNotification = await _context.Notifications.Where(_ => _.RecipientId == Guid.Parse(userId) && _.Content.Contains(query))
                    .Include(_ => _.StudentSender)
                    .Include(_ => _.Sender)
                    .ToListAsync();

                if (listNotification == null)
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
                    listData = listNotification
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

        public async Task<Logger> GetByStudentRecipientId(string studentId)
        {
            try
            {
                var listNotification = await _context.Notifications.Where(_ => _.StudentRecipientId == Guid.Parse(studentId))
                    .Include(_ => _.StudentSender)
                    .Include(_ => _.Sender)
                    .ToListAsync();

                if (listNotification == null)
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
                    listData = listNotification
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

        public async Task<Logger> SearchStudentRecipient(string studentId, string query)
        {
            try
            {
                var listNotification = await _context.Notifications.Where(_ => _.StudentRecipientId == Guid.Parse(studentId) && _.Content.Contains(query))
                    .Include(_ => _.StudentSender)
                    .Include(_ => _.Sender)
                    .ToListAsync();

                if (listNotification == null)
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
                    listData = listNotification
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

        public async Task<Logger> Update(Notification notification)
        {
            try
            {

                Notification extstNotification = await _context.Notifications.FindAsync(notification.Id);

                if (extstNotification == null)
                {
                    return new Logger()
                    {
                        status = TaskStatus.Faulted,
                        message = "Không tìm thấy đối tượng cần cập nhật"
                    };
                }

                extstNotification.Content = notification.Content;
                extstNotification.TimeCounter = notification.TimeCounter;
                extstNotification.RecipientId = notification.RecipientId;
                extstNotification.SenderId = notification.SenderId;
                extstNotification.IsRead = notification.IsRead;

                await _context.SaveChangesAsync();

                return new Logger()
                {
                    status = TaskStatus.RanToCompletion,
                    message = "Cập nhật thành công",
                    data = extstNotification
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
