using LMS_Library_API.Context;
using LMS_Library_API.Helpers;
using LMS_Library_API.Models;
using Microsoft.EntityFrameworkCore;

namespace LMS_Library_API.Services.StudentService
{
    public class StudentSvc:IStudentSvc
    {
        private readonly DataContext _context;
        private readonly IEncodeHelper _encodeHelper;

        public StudentSvc(DataContext context, IEncodeHelper encodeHelper)
        {
            _context = context;
            _encodeHelper = encodeHelper;
        }

        public async Task<Logger> Create(Student student)
        {
            try
            {
                var studentList = await _context.Students.ToListAsync();

                foreach (var item in studentList)
                {
                    if (string.Equals(student.Email, item.Email, StringComparison.OrdinalIgnoreCase))
                    {
                        return new Logger()
                        {
                            status = TaskStatus.Faulted,
                            message = "Email đã tồn tại",
                            data = student
                        };
                    }

                    if (string.Equals(student.PhoneNumber.Substring(student.PhoneNumber.Length - 9), item.PhoneNumber.Substring(item.PhoneNumber.Length - 9)))
                    {
                        return new Logger()
                        {
                            status = TaskStatus.Faulted,
                            message = "Số điện thoại đã tồn tại",
                            data = student
                        };
                    }
                }

                student.Password = _encodeHelper.Encode(student.Password);
                _context.Students.Add(student);
                await _context.SaveChangesAsync();
                return new Logger()
                {
                    status = TaskStatus.RanToCompletion,
                    message = "Thêm thành công",
                    data = student
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
                var respone = await _context.Students
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

        public async Task<Logger> GetById(string studentId)
        {
            try
            {
                Student existStudent = await _context.Students
                    .Include(_ => _.StudyTimes)
                    .Include(_ => _.StudyHistories)
                    .FirstOrDefaultAsync(x => x.Id == Guid.Parse(studentId));

                if (existStudent == null)
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
                    data = existStudent
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

        public async Task<Logger> Search(string query)
        {
            try
            {
                var respone = await _context.Students.Where(d => d.FullName.ToUpper().Contains(query) || d.Id.ToString().ToUpper().Contains(query) || d.Email.ToUpper().Contains(query)
                || d.PhoneNumber.ToUpper().Contains(query))
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

        public async Task<Logger> Update(Student student)
        {
            try
            {

                Student existStudent = await _context.Students.FindAsync(student.Id);

                if (existStudent == null)
                {
                    return new Logger()
                    {
                        status = TaskStatus.Faulted,
                        message = "Không tìm thấy đối tượng cần cập nhật"
                    };
                }


                if (student.Avartar == null)
                {
                    existStudent.Avartar = existStudent.Avartar;
                }
                else
                {
                    existStudent.Avartar = student.Avartar;
                }

                existStudent.FullName = student.FullName;
                existStudent.Email = student.Email;
                existStudent.DateOfBirth = student.DateOfBirth;
                existStudent.PhoneNumber = student.PhoneNumber;
                existStudent.Address = student.Address;
                existStudent.Gender = student.Gender;
                existStudent.Password = student.Password;
                existStudent.classId = student.classId;

                await _context.SaveChangesAsync();

                return new Logger()
                {
                    status = TaskStatus.RanToCompletion,
                    message = "Cập nhật thành công",
                    data = existStudent
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
