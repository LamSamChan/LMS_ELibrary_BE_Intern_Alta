using Aspose.Cells;
using Aspose.Words;
using LMS_Library_API.Context;
using LMS_Library_API.Models;
using LMS_Library_API.Models.AboutSubject;
using LMS_Library_API.Models.BlobStorage;
using LMS_Library_API.Models.Exams;
using Microsoft.EntityFrameworkCore;

namespace LMS_Library_API.Services.ExamService
{
    public class ExamSvc : IExamSvc
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

                exam.CensorId = null;
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

        public async Task<Logger> CreateMCExamByFile(string filePath)
        {
            List<char> Answer = new List<char>
            {
                'A','B','C','D','E','F','G','H', 'I', 'J', 'K'
            };

            try
            {
                Workbook workbook = new Workbook(filePath);

                Worksheet worksheet = workbook.Worksheets[0];

                Exam exam = new Exam();

                exam.Id = Guid.NewGuid().ToString().Substring(0, 4) + DateTime.Now.Ticks.ToString().Substring(7, 4);
                exam.FileType = Path.GetExtension(filePath);
                exam.FileName = worksheet.Cells["B2"].Value.ToString();
                exam.Format = true;
                exam.Duration = Convert.ToInt32(worksheet.Cells["B4"].Value);
                exam.ScoringScale = Convert.ToInt32(worksheet.Cells["B5"].Value);
                exam.DateCreated = Convert.ToDateTime(worksheet.Cells["B6"].Value);
                exam.DepartmentId = worksheet.Cells["B7"].Value.ToString().ToUpper();
                exam.SubjectId = worksheet.Cells["B8"].Value.ToString().ToUpper();

                switch (worksheet.Cells["B9"].Type)
                {
                    case Aspose.Cells.CellValueType.IsNull:
                        exam.Note = null;
                        break;
                    default:
                        exam.Note = worksheet.Cells["B9"].Value.ToString();
                        break;
                }

                exam.TeacherCreatedId = Guid.Parse(worksheet.Cells["B10"].Value.ToString());
                exam.Status = Enums.Status.PendingApproval;

                List<Question_Exam> listQuestion = new List<Question_Exam>();

                bool isEndQuestion = false;
                int indexQuestion = 1;
                
                do
                {       
                    QuestionBanks qBanks = new QuestionBanks();
                    qBanks.Format = true;
                    qBanks.Content = worksheet.Cells[$"E{indexQuestion}"].Value.ToString();
                    qBanks.LastUpdated = DateTime.Now;
                    qBanks.TeacherCreatedId = exam.TeacherCreatedId;
                    qBanks.SubjectId = exam.SubjectId;

                    string foreground = worksheet.Cells[$"E{indexQuestion}"].GetDisplayStyle().Font.Color.Name.ToUpper();

                    if (foreground == "FF0070C0")
                    {
                        qBanks.Level = Enums.Level.Medium;
                    }
                    else if (foreground == "FF7030A0")
                    {
                        qBanks.Level = Enums.Level.Hard;
                    }
                    else
                    {
                        qBanks.Level = Enums.Level.Easy;
                    }

                    Aspose.Cells.Cell cellStyle = worksheet.Cells[$"E{indexQuestion}"];
                    Aspose.Cells.Style style = cellStyle.GetStyle();
                    style.Font.Name = "Times New Roman";
                    style.Font.IsBold = true;
                    style.Font.Size = 12;
                    style.Font.Color = System.Drawing.Color.Black;
                    cellStyle.SetStyle(style);

                    //Answer
                    List<QB_Answer_MC> listAnswer = new List<QB_Answer_MC>();
                    bool isEndAnswer = false;

                    int indexAnswer = indexQuestion + 1;
                    do
                    {
                        QB_Answer_MC qB_Answer_MC = new QB_Answer_MC();
                        qB_Answer_MC.AnswerContent = worksheet.Cells[$"F{indexAnswer}"].Value.ToString();
                        string foregroundAnswer = worksheet.Cells[$"F{indexAnswer}"].GetDisplayStyle().Font.Color.Name.ToUpper();

                        qB_Answer_MC.IsCorrect = foregroundAnswer == "FFFF0000";
                        listAnswer.Add(qB_Answer_MC);

                        indexAnswer++;

                        switch (worksheet.Cells[$"F{indexAnswer}"].Type)
                        {
                            case Aspose.Cells.CellValueType.IsNull:
                                isEndAnswer = true;
                                break;
                        }

                    } while (!isEndAnswer);
                    
                    qBanks.QB_Answers_MC = listAnswer;

                    listQuestion.Add(new Question_Exam
                    {
                        QuestionBanks = qBanks
                    });

                    indexQuestion = indexAnswer + 1;
                    indexAnswer++;

                    switch (worksheet.Cells[$"E{indexQuestion}"].Type)
                    {
                        case Aspose.Cells.CellValueType.IsNull:
                            isEndQuestion = true;
                            break;
                    }

                } while (!isEndQuestion);

                exam.Question_Exam = listQuestion;

                var department = await _context.Departments.FindAsync(exam.DepartmentId.ToUpper());
                var subject = await _context.Subjects.FindAsync(exam.SubjectId.ToUpper());
                var teacherCreate = await _context.Users.FindAsync(exam.TeacherCreatedId);

                if (department == null)
                {
                    return new Logger()
                    {
                        status = TaskStatus.Faulted,
                        message = "Không tìm thấy tổ bộ môn của đề thi"
                    };
                }

                if (subject == null)
                {
                    return new Logger()
                    {
                        status = TaskStatus.Faulted,
                        message = "Không tìm thấy môn của đề thi"
                    };
                }

                if (teacherCreate == null)
                {
                    return new Logger()
                    {
                        status = TaskStatus.Faulted,
                        message = "Không tìm thấy giáo viên tạo đề thi"
                    };
                }
                //put value

                Aspose.Cells.Cell cellID = worksheet.Cells["B1"];
                cellID.PutValue(exam.Id);

                Aspose.Cells.Cell cellDuration = worksheet.Cells["B4"];
                cellDuration.PutValue(exam.Duration + " phút");

                Aspose.Cells.Cell cellDateCreate = worksheet.Cells["B6"];
                cellDateCreate.PutValue(exam.DateCreated.ToString("dd/MM/yyyy"));

                Aspose.Cells.Cell cellDepartment = worksheet.Cells["B7"];
                cellDepartment.PutValue(department.Name);

                Aspose.Cells.Cell cellSubject = worksheet.Cells["B8"];
                cellSubject.PutValue(subject.Name);

                Aspose.Cells.Cell cellTeacherCreate = worksheet.Cells["B10"];
                cellTeacherCreate.PutValue(teacherCreate.FullName);

                Aspose.Cells.Cell cellStatus = worksheet.Cells["B11"];
                cellStatus.PutValue("Đang chờ phê duyệt");

                worksheet.Name = exam.Id;
                workbook.Save(filePath);

                var loggerResult = await Create(exam);

                if (loggerResult.status == TaskStatus.RanToCompletion)
                {
                    return new Logger() { 
                        status = loggerResult.status,
                        message = loggerResult.message,
                        data = new BlobContentModel()
                        {
                            FileName = Path.GetFileName(filePath),
                            FilePath = filePath,
                            isImage = false
                        }
                    };
                }
                else
                {
                    return loggerResult;
                }

            }
            catch (Exception e)
            {

                return new Logger()
                {
                    status = TaskStatus.Faulted,
                    message = e.Message
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

        public async Task<Logger> GetById(string examId)
        {
            try
            {
                var existExam = await _context.Exams.FindAsync(examId);

                if (existExam == null)
                {
                    return new Logger()
                    {
                        status = TaskStatus.Faulted,
                        message = "Không tìm thấy đối tượng"
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

        public async Task<Logger> GetDetailExam(string examId, bool format)
        {
            try
            {
                Exam? existExam = new();

                if (format)
                {
                     existExam = await _context.Exams
                                .Include(_ => _.Question_Exam)
                                    .ThenInclude(_ => _.QuestionBanks)
                                        .ThenInclude(_ => _.QB_Answers_MC)
                                .FirstOrDefaultAsync(_ => _.Id == examId);
                }
                else
                {
                     existExam = await _context.Exams
                                .Include(_ => _.Question_Exam)
                                    .ThenInclude(_ => _.QuestionBanks)
                                        .ThenInclude(_ => _.QB_Answer_Essay)
                                .FirstOrDefaultAsync(_ => _.Id == examId);
                }

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

                Exam? existExam = await _context.Exams.Include(_ => _.Question_Exam).FirstOrDefaultAsync(_ => _.Id == exam.Id);

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
                existExam.ScoringScale = exam.ScoringScale;
                existExam.DateCreated = exam.DateCreated;
                existExam.FilePath = exam.FilePath;
                existExam.Status = exam.Status;
                existExam.Note = exam.Note;
                existExam.CensorId = exam.CensorId;
                existExam.TeacherCreated = exam.TeacherCreated;
                existExam.SubjectId = exam.SubjectId;
                existExam.DepartmentId = exam.DepartmentId;
                existExam.Question_Exam = exam.Question_Exam;

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
