using LMS_Library_API.Context;
using LMS_Library_API.Models;
using LMS_Library_API.Models.AboutStudent;
using Microsoft.EntityFrameworkCore;
using System;

namespace LMS_Library_API.Services.ServiceAboutStudent.StudentSubjectService
{
    public class StudentSubjectSvc : IStudentSubjectSvc
    {
        private readonly DataContext _context;

        public StudentSubjectSvc(DataContext context)
        {
            _context = context;
        }

        public async Task<Logger> Create(StudentSubject studentSubject)
        {
            try
            {
                _context.StudentSubjects.Add(studentSubject);
                await _context.SaveChangesAsync();
                return new Logger()
                {
                    status = TaskStatus.RanToCompletion,
                    message = "Thêm thành công",
                    data = studentSubject
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

        public async Task<Logger> Delete(string studentId, string subjectId)
        {
            try
            {
                var existStudentSubject = await _context.StudentSubjects.FirstOrDefaultAsync(_ => _.subjectId == subjectId && _.studentId == Guid.Parse(studentId));

                if (existStudentSubject == null)
                {
                    return new Logger()
                    {
                        status = TaskStatus.Faulted,
                        message = "Không tìm thấy đối tượng muốn xoá"
                    };
                }

                _context.Remove(existStudentSubject);
                await _context.SaveChangesAsync();
                return new Logger()
                {
                    status = TaskStatus.RanToCompletion,
                    message = "Xoá thành công",
                    data = existStudentSubject

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
                var respone = await _context.StudentSubjects.ToListAsync();
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

        public async Task<Logger> GetById(string studentId, string subjectId)
        {
            try
            {
                var existStudentSubject = await _context.StudentSubjects.Include(_ => _.Subject).FirstOrDefaultAsync(_ => _.subjectId == subjectId && _.studentId == Guid.Parse(studentId));

                if (existStudentSubject == null)
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
                    data = existStudentSubject
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
                var listStudentSubject = await _context.StudentSubjects.Include(_ => _.Subject).Where(_ => _.studentId == Guid.Parse(studentId)).ToListAsync();

                if (listStudentSubject == null)
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
                    listData = listStudentSubject
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

        public async Task<Logger> Update(StudentSubject studentSubject)
        {
            try
            {
                var existStudentSubject = await _context.StudentSubjects.FirstOrDefaultAsync(_ => _.subjectId == studentSubject.subjectId && _.studentId == studentSubject.studentId);

                if (existStudentSubject == null)
                {
                    return new Logger()
                    {
                        status = TaskStatus.Faulted,
                        message = "Không tìm thấy đối tượng cần cập nhật"
                    };
                }

                existStudentSubject.studentId = studentSubject.studentId;
                existStudentSubject.subjectId = studentSubject.subjectId;
                existStudentSubject.subjectMark = studentSubject.subjectMark;

                await _context.SaveChangesAsync();

                return new Logger()
                {
                    status = TaskStatus.RanToCompletion,
                    message = "Cập nhật thành công",
                    data = existStudentSubject
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
