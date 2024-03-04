using LMS_Library_API.Context;
using LMS_Library_API.Models;
using LMS_Library_API.Models.AboutStudent;
using Microsoft.EntityFrameworkCore;
using System;

namespace LMS_Library_API.Services.ServiceAboutSubject.ClassSubjectService
{
    public class ClassSubjectSvc : IClassSubjectSvc
    {
        private readonly DataContext _context;
        public ClassSubjectSvc(DataContext context) {
            _context = context; 
        }
        public async Task<Logger> Create(ClassSubject classSubject)
        {
            try
            {
                _context.ClassSubjects.Add(classSubject);
                await _context.SaveChangesAsync();
                return new Logger()
                {
                    status = TaskStatus.RanToCompletion,
                    message = "Thêm thành công",
                    data = classSubject
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

        public async Task<Logger> Delete(string classId, string subjectId)
        {
            try
            {
                var existClassSubject = await _context.ClassSubjects.FirstOrDefaultAsync(_ => _.classId == classId && _.subjectId == subjectId);

                if (existClassSubject == null)
                {
                    return new Logger()
                    {
                        status = TaskStatus.Faulted,
                        message = "Không tìm thấy đối tượng muốn xoá"
                    };
                }

                _context.Remove(existClassSubject);
                await _context.SaveChangesAsync();
                return new Logger()
                {
                    status = TaskStatus.RanToCompletion,
                    message = "Xoá thành công",
                    data = existClassSubject

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
                var respone = await _context.ClassSubjects.ToListAsync();
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
                var existClassSubject = await _context.ClassSubjects.Where(_ => _.classId == classId).ToListAsync();

                if (existClassSubject == null)
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
                    listData = existClassSubject
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

        public async Task<Logger> GetById(string classId, string subjectId)
        {
            try
            {
                var existClassSubject = await _context.ClassSubjects.FirstOrDefaultAsync(_ => _.classId == classId && _.subjectId == subjectId);

                if (existClassSubject == null)
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
                    data = existClassSubject
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

        public async Task<Logger> Update(ClassSubject classSubject)
        {
            try
            {
                var existClassSubject = await _context.ClassSubjects.FirstOrDefaultAsync(_ => _.classId == classSubject.classId && _.subjectId == classSubject.subjectId);

                if (existClassSubject == null)
                {
                    return new Logger()
                    {
                        status = TaskStatus.Faulted,
                        message = "Không tìm thấy đối tượng cần cập nhật"
                    };
                }

                existClassSubject.classId = classSubject.classId;
                existClassSubject.subjectId = classSubject.subjectId;

                await _context.SaveChangesAsync();

                return new Logger()
                {
                    status = TaskStatus.RanToCompletion,
                    message = "Cập nhật thành công",
                    data = existClassSubject
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
