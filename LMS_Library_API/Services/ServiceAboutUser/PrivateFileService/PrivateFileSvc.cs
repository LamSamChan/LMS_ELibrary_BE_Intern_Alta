using LMS_Library_API.Context;
using LMS_Library_API.Models;
using LMS_Library_API.Models.AboutUser;
using Microsoft.EntityFrameworkCore;

namespace LMS_Library_API.Services.ServiceAboutUser.PrivateFileService
{
    public class PrivateFileSvc : IPrivateFileSvc
    {
        private readonly DataContext _context;
        public PrivateFileSvc(DataContext context)
        {
            _context = context;
        }

        public async Task<Logger> Create(PrivateFile privateFile)
        {
            try
            {
                _context.PrivateFiles.Add(privateFile);
                await _context.SaveChangesAsync();
                return new Logger()
                {
                    status = TaskStatus.RanToCompletion,
                    message = "Thêm tệp thành công",
                    data = privateFile
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

        public async Task<Logger> Delete(int fileId)
        {
            try
            {
                var existFile = await _context.PrivateFiles.FindAsync(fileId);

                if (existFile == null)
                {
                    return new Logger()
                    {
                        status = TaskStatus.Faulted,
                        message = "Không tìm thấy đối tượng muốn xoá"
                    };
                }

                _context.Remove(existFile);
                await _context.SaveChangesAsync();
                return new Logger()
                {
                    status = TaskStatus.RanToCompletion,
                    message = "Xoá thành công",
                    data = existFile

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
                var respone = await _context.PrivateFiles.ToListAsync();
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

        public async Task<Logger> GetById(int fileId)
        {
            try
            {
                PrivateFile existFile = await _context.PrivateFiles.Include(_ => _.User).FirstOrDefaultAsync(x => x.Id == fileId);

                if (existFile == null)
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
                    data = existFile
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

        public async Task<Logger> GetByUserId(string userId)
        {
            try
            {
                var respone = await _context.PrivateFiles.Where(_ => _.UserId == Guid.Parse(userId)).ToListAsync();

                if (respone.Count == 0)
                {
                    return new Logger()
                    {
                        status = TaskStatus.RanToCompletion,
                        message = "Không có kết quả",
                    };
                }

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

        public async Task<Logger> Search(string userId, string query)
        {
            try
            {
                var respone = await _context.PrivateFiles.Where(_ => _.UserId == Guid.Parse(userId) &&
                _.Name.Contains(query)).ToListAsync();

                if (respone.Count == 0)
                {
                    return new Logger()
                    {
                        status = TaskStatus.RanToCompletion,
                        message = "Không có kết quả",
                    };
                }

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

        public async Task<Logger> Update(PrivateFile privateFile)
        {
            try
            {

                PrivateFile existFile = await _context.PrivateFiles.FindAsync(privateFile.Id);

                if (existFile == null)
                {
                    return new Logger()
                    {
                        status = TaskStatus.Faulted,
                        message = "Không tìm thấy đối tượng cần cập nhật"
                    };
                }
                
                existFile.Name = privateFile.Name;
                existFile.Modifier = privateFile.Modifier;
                existFile.DateChanged = DateTime.Now;
                existFile.FilePath = privateFile.FilePath;
                existFile.IsImage = privateFile.IsImage;

                await _context.SaveChangesAsync();

                return new Logger()
                {
                    status = TaskStatus.RanToCompletion,
                    message = "Cập nhật thành công",
                    data = existFile
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
