using LMS_Library_API.Context;
using LMS_Library_API.Models;
using LMS_Library_API.Models.AboutSubject;
using Microsoft.EntityFrameworkCore;

namespace LMS_Library_API.Services.ServiceAboutSubject.PartService
{
    public class PartSvc:IPartSvc
    {
        private readonly DataContext _context;

        public PartSvc(DataContext context)
        {
            _context = context;
        }

        public async Task<Logger> Create(Part part)
        {
            try
            {
                _context.Parts.Add(part);
                await _context.SaveChangesAsync();
                return new Logger()
                {
                    status = TaskStatus.RanToCompletion,
                    message = "Thêm thành công",
                    data = part
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

        public async Task<Logger> Delete(int partId)
        {
            try
            {
                var existPart = await _context.Parts.FindAsync(partId);

                if (existPart == null)
                {
                    return new Logger()
                    {
                        status = TaskStatus.Faulted,
                        message = "Không tìm thấy đối tượng muốn xoá"
                    };
                }

                _context.Remove(existPart);
                await _context.SaveChangesAsync();
                return new Logger()
                {
                    status = TaskStatus.RanToCompletion,
                    message = "Xoá thành công",
                    data = existPart

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
                var respone = await _context.Parts
                    .Include(_ => _.TeacherCreated)
                    .Include(_ => _.Censor)
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

        public async Task<Logger> GetById(int partId)
        {
            try
            {
                var existPart = await _context.Parts.Include(_ => _.Lessons).FirstOrDefaultAsync(_ => _.Id == partId);

                if (existPart == null)
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
                    data = existPart
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

        public async Task<Logger> Update(Part part)
        {
            try
            {

                Part existPart = await _context.Parts.FirstOrDefaultAsync(_ => _.Id == part.Id);

                if (existPart == null)
                {
                    return new Logger()
                    {
                        status = TaskStatus.Faulted,
                        message = "Không tìm thấy đối tượng cần cập nhật"
                    };
                }

                existPart.name = part.name;
                existPart.dateSubmited = DateTime.Now;
                existPart.isHidden = part.isHidden;
                existPart.numericalOrder = part.numericalOrder;
                existPart.censorId = part.censorId;
                existPart.teacherCreatedId = part.teacherCreatedId;
                existPart.subjectId = part.subjectId;
                existPart.note = part.note;
                existPart.status = part.status;

                await _context.SaveChangesAsync();

                return new Logger()
                {
                    status = TaskStatus.RanToCompletion,
                    message = "Cập nhật thành công",
                    data = existPart
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
