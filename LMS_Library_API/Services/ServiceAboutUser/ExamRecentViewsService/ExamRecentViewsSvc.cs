using Azure;
using LMS_Library_API.Context;
using LMS_Library_API.Models;
using LMS_Library_API.Models.AboutUser;
using LMS_Library_API.Models.Exams;
using Microsoft.EntityFrameworkCore;

namespace LMS_Library_API.Services.ServiceAboutUser.ExamRecentViewsService
{
    public class ExamRecentViewsSvc:IExamRecentViewsSvc
    {
        private readonly DataContext _context;

        public ExamRecentViewsSvc(DataContext context)
        {
            _context = context;
        }

        public async Task<Logger> Create(ExamRecentViews examRecentViews)
        {
            try
            {
                var countListByUser = await _context.ExamRecentViews.Where(_ => _.UserId == examRecentViews.UserId).ToListAsync();

                if (countListByUser.Count == 10)
                {
                    var deleteResult = await DeleteAtTen(countListByUser);

                    if (deleteResult.status == TaskStatus.Faulted)
                    {
                        return deleteResult;
                    }
                }

                _context.ExamRecentViews.Add(examRecentViews);
                await _context.SaveChangesAsync();
                return new Logger()
                {
                    status = TaskStatus.RanToCompletion,
                    message = "Thêm thành công",
                    data = examRecentViews
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

        private async Task<Logger> DeleteAtTen(List<ExamRecentViews> examRecentViews)
        {
            try
            {
                var existView = examRecentViews[9];

                if (existView == null)
                {
                    return new Logger()
                    {
                        status = TaskStatus.Faulted,
                        message = "Không tìm thấy đối tượng muốn xoá"
                    };
                }

                _context.Remove(existView);
                await _context.SaveChangesAsync();
                return new Logger()
                {
                    status = TaskStatus.RanToCompletion,
                    message = "Xoá thành công",
                    data = existView

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
                var respone = await _context.ExamRecentViews.ToListAsync();

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

        public async Task<Logger> GetById(string userId, string examId)
        {
            try
            {
                var existView = await _context.ExamRecentViews
                    .Include(_ => _.User)
                    .Include(_ => _.Exam)
                    .FirstOrDefaultAsync(x => x.ExamId == examId && x.UserId == Guid.Parse(userId));

                if (existView == null)
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
                    data = existView
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
                var existView = await _context.ExamRecentViews
                    .Include(_ => _.Exam)
                    .FirstOrDefaultAsync(x => x.UserId == Guid.Parse(userId));

                if (existView == null)
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
                    data = existView
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
