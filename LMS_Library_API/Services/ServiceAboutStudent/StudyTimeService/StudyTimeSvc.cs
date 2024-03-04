using LMS_Library_API.Context;
using LMS_Library_API.Models;
using LMS_Library_API.Models.AboutStudent;
using LMS_Library_API.Models.Notification;
using Microsoft.EntityFrameworkCore;

namespace LMS_Library_API.Services.ServiceAboutStudent.StudyTimeService
{
    public class StudyTimeSvc:IStudyTimeSvc
    {
        private readonly DataContext _context;

        public StudyTimeSvc(DataContext context)
        {
            _context = context;
        }

        public async Task<Logger> Create(StudyTime studyTime)
        {
            try
            {
                _context.StudyTimes.Add(studyTime);
                await _context.SaveChangesAsync();
                return new Logger()
                {
                    status = TaskStatus.RanToCompletion,
                    message = "Thêm thành công",
                    data = studyTime
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

        public async Task<Logger> Delete(string studentId, string subjectId, DateTime dateStudy)
        {
            try
            {
                var existTime = await _context.StudyTimes.FirstOrDefaultAsync(_ => _.subjectId == subjectId && _.studentId == Guid.Parse(studentId) && _.studyDate == dateStudy);

                if (existTime == null)
                {
                    return new Logger()
                    {
                        status = TaskStatus.Faulted,
                        message = "Không tìm thấy đối tượng muốn xoá"
                    };
                }

                _context.Remove(existTime);
                await _context.SaveChangesAsync();
                return new Logger()
                {
                    status = TaskStatus.RanToCompletion,
                    message = "Xoá thành công",
                    data = existTime

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
                var respone = await _context.StudyTimes.ToListAsync();
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

        public async Task<Logger> GetById(string studentId, string subjectId, DateTime dateStudy)
        {
            try
            {
                var existTime = await _context.StudyTimes.Include(_ => _.Subject).FirstOrDefaultAsync(_ => _.subjectId == subjectId && _.studentId == Guid.Parse(studentId) && _.studyDate == dateStudy);

                if (existTime == null)
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
                    data = existTime
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
                var studentStudyTime = await _context.StudyTimes.Include(_ => _.Subject).Where(_ => _.studentId == Guid.Parse(studentId)).OrderBy(_ => _.studyDate).ToListAsync();

                if (studentStudyTime == null)
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
                    listData = studentStudyTime
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

        public async Task<Logger> Update(StudyTime studyTime)
        {
            try
            {
                var existTime = await _context.StudyTimes.FirstOrDefaultAsync(_ => _.studentId == studyTime.studentId && _.subjectId == studyTime.subjectId && _.studyDate == studyTime.studyDate);

                if (existTime == null)
                {
                    return new Logger()
                    {
                        status = TaskStatus.Faulted,
                        message = "Không tìm thấy đối tượng cần cập nhật"
                    };
                }

                existTime.studentId = studyTime.studentId;
                existTime.subjectId = studyTime.subjectId;
                existTime.totalTime = studyTime.totalTime;
                existTime.studyDate = studyTime.studyDate;

                await _context.SaveChangesAsync();

                return new Logger()
                {
                    status = TaskStatus.RanToCompletion,
                    message = "Cập nhật thành công",
                    data = existTime
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
