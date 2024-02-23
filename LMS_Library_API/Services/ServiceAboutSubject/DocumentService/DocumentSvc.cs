using LMS_Library_API.Context;
using LMS_Library_API.Models;
using LMS_Library_API.Models.AboutSubject;
using Microsoft.EntityFrameworkCore;

namespace LMS_Library_API.Services.ServiceAboutSubject.DocumentService
{
    public class DocumentSvc:IDocumentSvc
    {
        private readonly DataContext _context;

        public DocumentSvc(DataContext context)
        {
            _context = context;
        }

        public async Task<Logger> Create(Document document)
        {
            try
            {
                var listDocByDocument = await _context.Documents.Where(d => d.lessonId == document.lessonId).ToListAsync();

                foreach (var item in listDocByDocument)
                {
                    if (item.Type)
                    {
                        return new Logger()
                        {
                            status = TaskStatus.Faulted,
                            message = "Bài học đã có bài giảng, không thể thêm bài giảng cho bài học này",
                            data = document
                        };
                    }
                }

                _context.Documents.Add(document);
                await _context.SaveChangesAsync();
                return new Logger()
                {
                    status = TaskStatus.RanToCompletion,
                    message = "Thêm thành công",
                    data = document
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
                var existDocument = await _context.Documents.FindAsync(documentId);

                if (existDocument == null)
                {
                    return new Logger()
                    {
                        status = TaskStatus.Faulted,
                        message = "Không tìm thấy đối tượng muốn xoá"
                    };
                }

                _context.Remove(existDocument);
                await _context.SaveChangesAsync();
                return new Logger()
                {
                    status = TaskStatus.RanToCompletion,
                    message = "Xoá thành công",
                    data = existDocument

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
                var respone = await _context.Documents
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

        public async Task<Logger> GetById(int documentId)
        {
            try
            {
                var existDocument = await _context.Documents
                    .Include(_ => _.TeacherCreated)
                    .Include(_ => _.Censor)
                    .FirstOrDefaultAsync(_ => _.Id == documentId);

                if (existDocument == null)
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
                    data = existDocument
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

        public async Task<Logger> Update(Document document)
        {
            try
            {

                Document existDocument = await _context.Documents.FindAsync(document.Id);

                if (existDocument == null)
                {
                    return new Logger()
                    {
                        status = TaskStatus.Faulted,
                        message = "Không tìm thấy đối tượng cần cập nhật"
                    };
                }

                var listDocByDocument = await _context.Documents.Where(d => d.lessonId == document.lessonId).ToListAsync();

                foreach (var item in listDocByDocument)
                {
                    if (item.Type)
                    {
                        return new Logger()
                        {
                            status = TaskStatus.Faulted,
                            message = "Bài học đã có bài giảng, không thể thêm bài giảng cho bài học này",
                            data = document
                        };
                    }
                }

                if (document.FilePath == null)
                {
                    existDocument.FilePath = existDocument.FilePath;
                }
                else
                {
                    existDocument.FilePath = document.FilePath;
                }

                existDocument.Name = document.Name;
                existDocument.Type = document.Type;
                existDocument.submissionDate = document.submissionDate;
                existDocument.updatedDate = DateTime.Now;
                existDocument.status = document.status;
                existDocument.note = document.note;
                existDocument.lessonId = document.lessonId;
                existDocument.teacherCreatedId = document.teacherCreatedId;
                existDocument.censorId = document.censorId;

                await _context.SaveChangesAsync();

                return new Logger()
                {
                    status = TaskStatus.RanToCompletion,
                    message = "Cập nhật thành công",
                    data = existDocument
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
