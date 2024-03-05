using LMS_Library_API.Context;
using LMS_Library_API.Models;
using LMS_Library_API.Models.AboutSubject;
using Microsoft.EntityFrameworkCore;

namespace LMS_Library_API.Services.ServiceAboutSubject.SubjectNotificationService
{
    public class SubjectNotificationSvc : ISubjectNotificationSvc
    {
        private readonly DataContext _context;

        public SubjectNotificationSvc(DataContext context)
        {
            _context = context;
        }

        public async Task<Logger> Create(SubjectNotification subjectNotification)
        {
            try
            {
                _context.SubjectNotifications.Add(subjectNotification);
                await _context.SaveChangesAsync();
                return new Logger()
                {
                    status = TaskStatus.RanToCompletion,
                    message = "Thêm thành công",
                    data = subjectNotification
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

        public async Task<Logger> Delete(int subjectNotificationId)
        {
            try
            {
                var existNotification = await _context.SubjectNotifications.FindAsync(subjectNotificationId);

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
                var respone = await _context.SubjectNotifications.ToListAsync();
                return new Logger()
                {
                    status = TaskStatus.RanToCompletion,
                    message = "Thành công",
                    listData = respone
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

        public async Task<Logger> GetById(int subjectNotificationId)
        {
            try
            {
                var existNotification = await _context.SubjectNotifications.Include(_ => _.Subject).FirstOrDefaultAsync(_ => _.Id == subjectNotificationId);

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

        public async Task<Logger> GetBySubjectId(string subjectId)
        {
            try
            {
                var existNotification = await _context.SubjectNotifications.Where(_ => _.subjectId == subjectId).ToListAsync();

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

        public async Task<Logger> GetByTeacherId(string userId)
        {
            try
            {
                var existNotification = await _context.SubjectNotifications.Where(_ => _.teacherId == Guid.Parse(userId)).ToListAsync();

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

        public async Task<Logger> Update(SubjectNotification subjectNotification)
        {
            try
            {

                var existNotification = await _context.SubjectNotifications.FindAsync(subjectNotification.Id);

                if (existNotification == null)
                {
                    return new Logger()
                    {
                        status = TaskStatus.Faulted,
                        message = "Không tìm thấy đối tượng cần cập nhật"
                    };
                }

                existNotification.Id = subjectNotification.Id;
                existNotification.title = subjectNotification.title;
                existNotification.CreatedDate = subjectNotification.CreatedDate;
                existNotification.content = subjectNotification.content;
                existNotification.subjectId = subjectNotification.subjectId;    
                existNotification.teacherId = subjectNotification.teacherId;

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
