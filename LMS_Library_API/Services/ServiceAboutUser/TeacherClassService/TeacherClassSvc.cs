using LMS_Library_API.Context;
using LMS_Library_API.Models;
using LMS_Library_API.Models.AboutStudent;
using LMS_Library_API.Models.AboutUser;
using Microsoft.EntityFrameworkCore;

namespace LMS_Library_API.Services.ServiceAboutUser.TeacherClassService
{
    public class TeacherClassSvc : ITeacherClassSvc
    {
        private readonly DataContext _context;

        public TeacherClassSvc(DataContext context)
        {
            _context = context;   
        }

        public async Task<Logger> Create(TeacherClass teacherClass)
        {
            try
            {
                _context.TeacherClasses.Add(teacherClass);
                await _context.SaveChangesAsync();
                return new Logger()
                {
                    status = TaskStatus.RanToCompletion,
                    message = "Thêm thành công",
                    data = teacherClass
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

        public async Task<Logger> Delete(string userId, string classId)
        {
            try
            {
                var existTeacherClass = await _context.TeacherClasses.FirstOrDefaultAsync(_ => _.classId == classId && _.teacherId == Guid.Parse(userId));

                if (existTeacherClass == null)
                {
                    return new Logger()
                    {
                        status = TaskStatus.Faulted,
                        message = "Không tìm thấy đối tượng muốn xoá"
                    };
                }

                _context.Remove(existTeacherClass);
                await _context.SaveChangesAsync();
                return new Logger()
                {
                    status = TaskStatus.RanToCompletion,
                    message = "Xoá thành công",
                    data = existTeacherClass

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
                var respone = await _context.TeacherClasses.ToListAsync();
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

        public async Task<Logger> GetById(string userId, string classId)
        {
            try
            {
                var existTeacherClass = await _context.TeacherClasses.Include(_ => _.Class).FirstOrDefaultAsync(_ => _.classId == classId && _.teacherId == Guid.Parse(userId));

                if (existTeacherClass == null)
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
                    data = existTeacherClass
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
                var listTeacherClass = await _context.TeacherClasses.Include(_ => _.Class).Where(_ => _.teacherId == Guid.Parse(userId)).ToListAsync();

                if (listTeacherClass == null)
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
                    listData = listTeacherClass
                };
            }
            catch (Exception ex)
            {
                return new Logger()
                {
                    status = TaskStatus.Faulted,
                    message = ex.Message
                };
            }
        }

        public async Task<Logger> Update(TeacherClass teacherClass)
        {
            try
            {
                var existTeacherClass = await _context.TeacherClasses.FirstOrDefaultAsync(_ => _.teacherId == teacherClass.teacherId);

                if (existTeacherClass == null)
                {
                    return new Logger()
                    {
                        status = TaskStatus.Faulted,
                        message = "Không tìm thấy đối tượng cần cập nhật"
                    };
                }

                existTeacherClass.teacherId = teacherClass.teacherId;
                existTeacherClass.classId = teacherClass.classId;

                await _context.SaveChangesAsync();

                return new Logger()
                {
                    status = TaskStatus.RanToCompletion,
                    message = "Cập nhật thành công",
                    data = existTeacherClass
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
