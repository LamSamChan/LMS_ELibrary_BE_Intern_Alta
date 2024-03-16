using LMS_Library_API.Context;
using LMS_Library_API.Models;
using LMS_Library_API.Models.AboutStudent;
using LMS_Library_API.Models.AboutSubject;
using Microsoft.EntityFrameworkCore;

namespace LMS_Library_API.Services.ServiceAboutSubject.NotificationClassStudentService
{
    public class NotificationClassStudentSvc: INotificationClassStudentSvc
    {
        private readonly DataContext _context;

        public NotificationClassStudentSvc(DataContext context)
        {
            _context = context;
        }

        public async Task<Logger> Create(NotificationClassStudent notification)
        {
            try
            {
                if (notification.StudentId != null)
                {
                    notification.IsForAllStudent = true;
                }
                else
                {
                    notification.IsForAllStudent = false;

                }

                _context.NotificationClassStudents.Add(notification);
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

        public async Task<Logger> Delete(int subjectNotificationId, string classId, Guid? studentId)
        {
            try
            {
                var existNotification = await _context.NotificationClassStudents.FirstOrDefaultAsync(_ => _.SubjectNotificationId == subjectNotificationId 
                && _.ClassId == classId && _.StudentId == studentId);

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
                var respone = await _context.NotificationClassStudents.ToListAsync();
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

        public async Task<Logger> GetByClassId(string classId)
        {
            try
            {
                var existNotification = await _context.NotificationClassStudents.Include(_ => _.SubjectNotification)
                    .FirstOrDefaultAsync(_ => _.ClassId == classId);

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

        public async Task<Logger> GetById(int subjectNotificationId, string classId, Guid? studentId)
        {
            try
            {
                var existNotification = await _context.NotificationClassStudents.FirstOrDefaultAsync(_ => _.SubjectNotificationId == subjectNotificationId
                && _.ClassId == classId && _.StudentId == studentId);

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

        public async Task<Logger> GetByStudentId(string studentId)
        {
            try
            {
                var existNotification = await _context.NotificationClassStudents.Include(_ => _.SubjectNotification)
                    .FirstOrDefaultAsync(_ => _.StudentId == Guid.Parse(studentId));

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

        public async Task<Logger> Update(NotificationClassStudent notification)
        {
            try
            {
                var existNotification = await _context.NotificationClassStudents
                    .FirstOrDefaultAsync(_ => _.SubjectNotificationId == notification.SubjectNotificationId
                && _.ClassId == notification.ClassId && _.StudentId == notification.StudentId);

                if (existNotification == null)
                {
                    return new Logger()
                    {
                        status = TaskStatus.Faulted,
                        message = "Không tìm thấy đối tượng cần cập nhật"
                    };
                }

                existNotification.SubjectNotificationId = notification.SubjectNotificationId;
                existNotification.ClassId = notification.ClassId;
                existNotification.StudentId = notification.StudentId;
                existNotification.IsForAllStudent = notification.IsForAllStudent;

                if (notification.StudentId != null)
                {
                    existNotification.IsForAllStudent = true;
                }
                else
                {
                    existNotification.IsForAllStudent = false;

                }

                await _context.SaveChangesAsync();

                return new Logger()
                {
                    status = TaskStatus.RanToCompletion,
                    message = "Cập nhật thành công",
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
    }
}
