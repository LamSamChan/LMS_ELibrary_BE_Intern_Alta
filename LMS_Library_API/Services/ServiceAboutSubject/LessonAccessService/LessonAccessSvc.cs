// Ignore Spelling: API LMS Svc

using LMS_Library_API.Context;
using LMS_Library_API.Models;
using LMS_Library_API.Models.AboutStudent;
using LMS_Library_API.Models.AboutSubject;
using Microsoft.EntityFrameworkCore;

namespace LMS_Library_API.Services.ServiceAboutSubject.LessonAccessService
{
    public class LessonAccessSvc: ILessonAccessSvc
    {
        private readonly DataContext _context;

        public LessonAccessSvc(DataContext context)
        {
            _context = context;
        }

        public async Task<Logger> Create(LessonAccess lessonAccess)
        {
            try
            {
                if (String.IsNullOrEmpty(lessonAccess.classId))
                {
                    lessonAccess.isForAllClasses = true;
                }
                else
                {
                    lessonAccess.isForAllClasses = false;
                }

                _context.LessonAccess.Add(lessonAccess);
                await _context.SaveChangesAsync();
                return new Logger()
                {
                    status = TaskStatus.RanToCompletion,
                    message = "Thêm thành công",
                    data = lessonAccess
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

        public async Task<Logger> Delete(int lessonId)
        {
            try
            {
                var extstAccess = await _context.LessonAccess.FirstOrDefaultAsync(_ => _.lessonId == lessonId);

                if (extstAccess == null)
                {
                    return new Logger()
                    {
                        status = TaskStatus.Faulted,
                        message = "Không tìm thấy đối tượng muốn xoá"
                    };
                }

                _context.Remove(extstAccess);
                await _context.SaveChangesAsync();
                return new Logger()
                {
                    status = TaskStatus.RanToCompletion,
                    message = "Xoá thành công",
                    data = extstAccess

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
                var respone = await _context.LessonAccess.ToListAsync();
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
                var extstAccess = await _context.LessonAccess.Include(_ => _.Lesson).Where(_ => _.classId == classId).ToListAsync();

                if (extstAccess == null)
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
                    listData = extstAccess
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

        public async Task<Logger> GetById(int lessonId)
        {
            try
            {
                var existAccess = await _context.LessonAccess.Include(_ => _.Lesson).FirstOrDefaultAsync(_ => _.lessonId == lessonId);

                if (existAccess == null)
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
                    data = existAccess
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

        public async Task<Logger> Update(LessonAccess lessonAccess)
        {
            try
            {
                var existAccess = await _context.LessonAccess.FindAsync(lessonAccess.lessonId);

                if (existAccess == null)
                {
                    return new Logger()
                    {
                        status = TaskStatus.Faulted,
                        message = "Không tìm thấy đối tượng cần cập nhật"
                    };
                }

                if (String.IsNullOrEmpty(lessonAccess.classId))
                {
                    existAccess.isForAllClasses = true;
                }
                else
                {
                    existAccess.isForAllClasses = false;
                }

                existAccess.lessonId = existAccess.lessonId;
                existAccess.classId = existAccess.classId;

                await _context.SaveChangesAsync();

                return new Logger()
                {
                    status = TaskStatus.RanToCompletion,
                    message = "Cập nhật thành công",
                    data = existAccess
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
