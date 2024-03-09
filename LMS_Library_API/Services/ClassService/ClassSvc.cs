using Azure;
using LMS_Library_API.Context;
using LMS_Library_API.Models;
using LMS_Library_API.Models.Exams;
using LMS_Library_API.Services.ServiceAboutExam.QuestionBankService;
using Microsoft.EntityFrameworkCore;

namespace LMS_Library_API.Services.ClassService
{
    public class ClassSvc:IClassSvc
    {
        private readonly DataContext _context;
        
        public ClassSvc(DataContext context)
        {
            _context = context;
        }

        public async Task<int> CountStudentInClass(string classId)
        {
            var studentList = await _context.Students.Where(s => s.classId.ToLower() == classId.ToLower()).ToListAsync();

            return studentList.Count();
        }

        public async Task<Logger> Create(Class classModel)
        {
            try
            {
                foreach (var item in await _context.Classes.ToListAsync())
                {
                    if (item.Id == classModel.Id.ToUpper())
                    {
                        return new Logger()
                        {
                            status = TaskStatus.Faulted,
                            message = "Mã lớp đã tồn tại"
                        };
                    }
                }

                _context.Classes.Add(classModel);
                await _context.SaveChangesAsync();
                return new Logger()
                {
                    status = TaskStatus.RanToCompletion,
                    message = "Thêm thành công",
                    data = classModel
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

        public async Task<Logger> Delete(string classId)
        {
            try
            {
                var existClass = await _context.Classes.FindAsync(classId);

                if (existClass == null)
                {
                    return new Logger()
                    {
                        status = TaskStatus.Faulted,
                        message = "Không tìm thấy đối tượng muốn xoá"
                    };
                }

                _context.Remove(existClass);
                await _context.SaveChangesAsync();
                return new Logger()
                {
                    status = TaskStatus.RanToCompletion,
                    message = "Xoá thành công",
                    data = existClass

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
                var respone = await _context.Classes.ToListAsync();

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

        public async Task<Logger> GetById(string classId)
        {
            try
            {
                Class? existClass = await _context.Classes
                    .Include(_ => _.Students)
                    .Include(_ => _.TeacherClasses).ThenInclude(_ => _.Teacher)
                    .FirstOrDefaultAsync(x =>x.Id == classId.ToUpper());

                if (existClass == null)
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
                    data = existClass
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
                var existListClass = await _context.Classes.Where(_ => _.name.Contains(query) || _.Id.Contains(query)).ToListAsync();

                if (existListClass == null)
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
                    data = existListClass
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

        public async Task<Logger> Update(Class classModel)
        {
            try
            {

                Class? existClass = await _context.Classes.FirstOrDefaultAsync(x => x.Id == classModel.Id);

                if (existClass == null)
                {
                    return new Logger()
                    {
                        status = TaskStatus.Faulted,
                        message = "Không tìm thấy đối tượng cần cập nhật"
                    };
                }

                existClass.name = classModel.name;
                existClass.totalStudent = classModel.totalStudent;

                await _context.SaveChangesAsync();

                return new Logger()
                {
                    status = TaskStatus.RanToCompletion,
                    message = "Cập nhật thành công",
                    data = existClass
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
