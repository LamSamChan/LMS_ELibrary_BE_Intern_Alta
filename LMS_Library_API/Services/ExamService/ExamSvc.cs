using LMS_Library_API.Context;
using LMS_Library_API.Models;
using LMS_Library_API.Models.AboutSubject;
using LMS_Library_API.Models.Exams;
using Microsoft.EntityFrameworkCore;

namespace LMS_Library_API.Services.ExamService
{
    public class ExamSvc:IExamSvc
    {
        private readonly DataContext _context;

        public ExamSvc(DataContext context)
        {
            _context = context;
        }

        public async Task<Logger> Create(Exam exam)
        {
            try
            {
                foreach (var item in await GetCheck(exam.SubjectId))
                {
                    if (string.Equals(exam.Id, item.Id, StringComparison.OrdinalIgnoreCase))
                    {
                        return new Logger()
                        {
                            status = TaskStatus.Faulted,
                            message = "Mã đề thi đã tồn tại trong môn học này",
                            data = exam
                        };
                    }
                }

                exam.Id = exam.Id.ToUpper();

                _context.Exams.Add(exam);
                await _context.SaveChangesAsync();
                return new Logger()
                {
                    status = TaskStatus.RanToCompletion,
                    message = "Thêm thành công",
                    data = exam
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

        public async Task<Logger> Delete(string examId)
        {
            try
            {
                var existExam = await _context.Exams.FindAsync(examId);

                if (existExam == null)
                {
                    return new Logger()
                    {
                        status = TaskStatus.Faulted,
                        message = "Không tìm thấy đối tượng muốn xoá"
                    };
                }

                _context.Remove(existExam);
                await _context.SaveChangesAsync();
                return new Logger()
                {
                    status = TaskStatus.RanToCompletion,
                    message = "Xoá thành công",
                    data = existExam
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
                var respone = await _context.Exams
                    .Include(_ => _.TeacherCreated)
                    .Include(_ => _.Subject)
                    .Include(_ => _.Department)
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

        public async Task<Logger> GetDetailExam(string examId)
        {
            try
            {
                
                var existExam = await _context.Exams
                                .Include(_ => _.Question_Exam)
                                    .ThenInclude(_ => _.QuestionBanks)
                                        .ThenInclude(_ => _.QB_Answers_MC)
                                 .Include(_ => _.Question_Exam)
                                    .ThenInclude(_ => _.QuestionBanks)
                                        .ThenInclude(_ => _.QB_Answer_Essay)
                                .Where(x => x.Id == examId).ToListAsync();

                if (existExam == null)
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
                    data = existExam
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
                var respone = await _context.Exams
                    .Include(_ => _.TeacherCreated)
                    .Include(_ => _.Subject)
                    .Include(_ => _.Department)
                    .Where(e => e.FileName.Contains(query))
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

        public async Task<Logger> Update(Exam exam)
        {
            try
            {

                Exam? existExam = await _context.Exams.FirstOrDefaultAsync(_ => _.Id == exam.Id);

                if (existExam == null)
                {
                    return new Logger()
                    {
                        status = TaskStatus.Faulted,
                        message = "Không tìm thấy đối tượng cần cập nhật"
                    };
                }

                existExam.FileType = exam.FileType;
                existExam.FileName = exam.FileName;
                existExam.Format = exam.Format;
                existExam.Duration = exam.Duration;
                existExam.DateCreated = exam.DateCreated;
                existExam.FilePath = exam.FileName;
                existExam.Status = exam.Status;
                existExam.Note = exam.Note;
                existExam.CensorId = exam.CensorId;
                existExam.TeacherCreated = exam.TeacherCreated;
                existExam.SubjectId = exam.SubjectId;
                existExam.DepartmentId = exam.DepartmentId;

                await _context.SaveChangesAsync();

                return new Logger()
                {
                    status = TaskStatus.RanToCompletion,
                    message = "Cập nhật thành công",
                    data = existExam
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

        private async Task<IEnumerable<Exam>> GetCheck(string subjectId)
        {
            try
            {
                var respone = await _context.Exams.Where(e => e.SubjectId == subjectId).ToListAsync();
                return respone;
            }
            catch (Exception)
            {
                return new List<Exam>();
            }
        }
    }
}
