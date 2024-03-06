using LMS_Library_API.Context;
using LMS_Library_API.Models;
using LMS_Library_API.Models.AboutSubject;
using Microsoft.EntityFrameworkCore;

namespace LMS_Library_API.Services.ServiceAboutSubject.DocumentAccessService
{
    public class DocumentAccessSvc:IDocumentAccessSvc
    {
        private readonly DataContext _context;

        public DocumentAccessSvc(DataContext context)
        {
            _context = context;
        }

        public async Task<Logger> Create(DocumentAccess documentAccess)
        {
            try
            {
                if (String.IsNullOrEmpty(documentAccess.classId))
                {
                    documentAccess.isForAllClasses = true;
                }
                else
                {
                    documentAccess.isForAllClasses = false;
                }

                _context.DocumentAccess.Add(documentAccess);
                await _context.SaveChangesAsync();
                return new Logger()
                {
                    status = TaskStatus.RanToCompletion,
                    message = "Thêm thành công",
                    data = documentAccess
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

        public async Task<Logger> Delete(int documentId)
        {
            try
            {
                var existAccess = await _context.DocumentAccess.FirstOrDefaultAsync(_ =>  _.documentId == documentId);

                if (existAccess == null)
                {
                    return new Logger()
                    {
                        status = TaskStatus.Faulted,
                        message = "Không tìm thấy đối tượng muốn xoá"
                    };
                }

                _context.Remove(existAccess);
                await _context.SaveChangesAsync();
                return new Logger()
                {
                    status = TaskStatus.RanToCompletion,
                    message = "Xoá thành công",
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

        public async Task<Logger> GetAll()
        {
            try
            {
                var respone = await _context.DocumentAccess.ToListAsync();
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
                var existAccess = await _context.DocumentAccess.Include(_ => _.Document).Where(_ => _.classId == classId).ToListAsync();

                if (existAccess == null)
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
                    listData = existAccess
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

        public async Task<Logger> GetById(int documentId)
        {
            try
            {
                var existClassSubject = await _context.DocumentAccess.Include(_ => _.Document).FirstOrDefaultAsync(_ =>  _.documentId == documentId);

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

        public async Task<Logger> Update(DocumentAccess documentAccess)
        {
            try
            {
                var existAccess = await _context.DocumentAccess.FindAsync(documentAccess.documentId);

                if (existAccess == null)
                {
                    return new Logger()
                    {
                        status = TaskStatus.Faulted,
                        message = "Không tìm thấy đối tượng cần cập nhật"
                    };
                }

                if (String.IsNullOrEmpty(documentAccess.classId))
                {
                    existAccess.isForAllClasses = true;
                }
                else
                {
                    existAccess.isForAllClasses = false;
                }

                existAccess.documentId = existAccess.documentId;
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
