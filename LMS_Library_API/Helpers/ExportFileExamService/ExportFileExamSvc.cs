using Aspose.Cells;
using Aspose.Words;
using LMS_Library_API.Context;
using LMS_Library_API.Models.BlobStorage;
using LMS_Library_API.Models.Exams;
using System.Diagnostics.Contracts;
using System.Drawing;
using System.Reflection.Metadata.Ecma335;
using ExcelBorderType = Aspose.Cells.BorderType;
using ExcelSaveFormat = Aspose.Cells.SaveFormat;
using WordSaveFormat = Aspose.Cells.SaveFormat;
using WordBorderType = Aspose.Words.BorderType;
using Microsoft.OpenApi.Extensions;
using Aspose.Words.Tables;
using Microsoft.AspNetCore.Components;

namespace LMS_Library_API.Helpers.ExportFileExamService
{
    public class ExportFileExamSvc : IExportFileExamSvc
    {
        private readonly DataContext _context;

        public ExportFileExamSvc(DataContext context)
        {
            _context = context;
        }

        private List<string> title = new List<string>
        {
            "ID", "Tên đề thi","Hình thức", "Thời gian", "Thang điểm", "Ngày tạo", "Tổ bộ môn", "Môn học", "Ghi chú", "Giảng viên tạo", "Trạng thái"
        };

        public async Task<BlobContentModel> ExportExamToExcel(Exam exam)
        {
            List<char> Answer = new List<char>
            {
                'A','B','C','D','E','F','G','H', 'I', 'J', 'K'
            };

            var department = await _context.Departments.FindAsync(exam.DepartmentId.ToUpper());
            var subject = await _context.Subjects.FindAsync(exam.SubjectId.ToUpper());
            var teacherCreate = await _context.Users.FindAsync(exam.TeacherCreatedId);

            string status = "";
            if (exam.Status == Enums.Status.Unsubmitted)
            {
                status = "Chưa gửi phê duyệt";
            }
            else if (exam.Status == Enums.Status.PendingApproval)
            {
                status = "Đang chờ phê duyệt";
            }
            else if (exam.Status == Enums.Status.Approved)
            {
                status = "Đã phê duyệt";
            }
            else if (exam.Status == Enums.Status.RefuseApproval)
            {
                status = "Đã từ chối phê duyệt";
            }
            else if (exam.Status == Enums.Status.CancelApproval)
            {
                status = "Đã hủy phê duyệt";
            }
            else
            {
                status = "Lưu nháp";
            }

            List<string> value = new List<string>
            {
                $"{exam.Id}", $"{exam.FileName}","Trắc nghiệm", $"{exam.Duration} phút", $"{exam.ScoringScale}", $"{exam.DateCreated.ToString("dd/MM/yyyy")}", 
                $"{department?.Name}",  $"{subject?.Name}", $"{exam.Note}",  $"{teacherCreate?.FullName}", $"{status}"
            };
            // Instantiate a Workbook object that represents Excel file.
            Workbook wb = new Workbook();

            // When you create a new workbook, a default "Sheet1" is added to the workbook.
            Worksheet sheet = wb.Worksheets[0];
            sheet.Name = exam.Id;
            sheet.Cells.SetColumnWidthPixel(0, 150);
            sheet.Cells.SetColumnWidthPixel(1, 300);          

            // Set style
            StyleFlag flag = new StyleFlag();
            flag.All = true;

            StyleFlag flagBold = new StyleFlag();
            flagBold.FontBold = true;

            Aspose.Cells.Cell columnA = sheet.Cells["A1"];

            Aspose.Cells.Style style= columnA.GetStyle();
            style.Font.Name = "Times New Roman";
            style.Font.IsBold = true;
            style.Font.Size = 12;
            style.HorizontalAlignment = TextAlignmentType.Left;
            style.VerticalAlignment = TextAlignmentType.Center;

            //Setting the color
            style.Borders[ExcelBorderType.BottomBorder].Color = Color.Black;
            style.Borders[ExcelBorderType.TopBorder].Color = Color.Black;
            style.Borders[ExcelBorderType.LeftBorder].Color = Color.Black;
            style.Borders[ExcelBorderType.RightBorder].Color = Color.Black;
            // Setting the line style
            style.Borders[ExcelBorderType.BottomBorder].LineStyle = CellBorderType.Thin;
            style.Borders[ExcelBorderType.TopBorder].LineStyle = CellBorderType.Thin;
            style.Borders[ExcelBorderType.LeftBorder].LineStyle = CellBorderType.Thin;
            style.Borders[ExcelBorderType.RightBorder].LineStyle = CellBorderType.Thin;

            Aspose.Cells.Style styleColumnB = columnA.GetStyle();
            styleColumnB.Font.IsBold = false;

            sheet.Cells.CreateRange("A1:B11").ApplyStyle(style, flag);
            sheet.Cells.CreateRange("B1:B11").ApplyStyle(styleColumnB, flagBold);

            //Put value to Column A and add style
            for (int i = 0; i < title.Count; i++)
            {
                Aspose.Cells.Cell cellA = sheet.Cells[$"A{i+1}"];
                cellA.PutValue($"{title[i]}:");

                Aspose.Cells.Cell cellB = sheet.Cells[$"B{i + 1}"];
                cellB.PutValue($"{value[i]}");
            }


            //Question

            //Put Value

            List<Question_Exam> listQuestion = exam.Question_Exam.ToList();
            int questionCount = listQuestion.Count();

            int cellIndex = 1;
            for (int i = 0; i < questionCount; i++)
            {
                //Setting
                sheet.Cells.Merge(cellIndex - 1, 4, 1, 14);

                Aspose.Cells.Cell columnTitle = sheet.Cells[$"D{cellIndex}"];
                Aspose.Cells.Style styleTitleQuestion = columnTitle.GetStyle();
                styleTitleQuestion.Font.Name = "Times New Roman";
                styleTitleQuestion.Font.IsBold = true;
                styleTitleQuestion.Font.Size = 12;
                styleTitleQuestion.HorizontalAlignment = TextAlignmentType.Left;
                styleTitleQuestion.VerticalAlignment = TextAlignmentType.Center;
                styleTitleQuestion.IsTextWrapped = true;
                sheet.Cells.CreateRange($"D{cellIndex}:E{cellIndex}").ApplyStyle(styleTitleQuestion, flag);

                Aspose.Cells.Cell cellDQuestion = sheet.Cells[$"D{cellIndex}"];
                cellDQuestion.PutValue($"Câu {i + 1}:");
      
                Aspose.Cells.Cell cellEQuestion = sheet.Cells[$"E{cellIndex}"];
                cellEQuestion.PutValue($"{listQuestion[i].QuestionBanks.Content}");

                var listAnswer = listQuestion[i].QuestionBanks.QB_Answers_MC.ToList();

                for (int j = 0; j < listAnswer.Count; j++)
                {
                    Aspose.Cells.Cell cellEAnswer = sheet.Cells[$"E{cellIndex + 1 + j}"];
                    cellEAnswer.PutValue($"{Answer[j]}.");

                    Aspose.Cells.Cell cellFAnswer = sheet.Cells[$"F{cellIndex + 1 + j}"];
                    cellFAnswer.PutValue($"{listAnswer[j].AnswerContent}.");

                    Aspose.Cells.Style styleAnswer = cellEAnswer.GetStyle();
                    styleAnswer.Font.Name = "Times New Roman";
                    styleAnswer.Font.Size = 12;
                    styleAnswer.HorizontalAlignment = TextAlignmentType.Left;
                    styleAnswer.VerticalAlignment = TextAlignmentType.Center;
                    sheet.Cells.CreateRange($"E{cellIndex + 1 + j}:F{cellIndex + 1 + j}").ApplyStyle(styleAnswer, flag);

                    if (listAnswer[j].IsCorrect)
                    {
                        Aspose.Cells.Style styleCorrect = cellFAnswer.GetStyle();
                        styleCorrect.Font.Color = Color.Red;
                        sheet.Cells.CreateRange($"E{cellIndex + 1 + j}:F{cellIndex + 1 + j}").ApplyStyle(styleCorrect, flag);
                    }
                }

                cellIndex += listAnswer.Count() + 2;
            }

            // Save the Excel as .xlsx file.
            string fileName = $"{exam.Id}.xlsx";
            wb.Save(fileName, ExcelSaveFormat.Xlsx);

            return new BlobContentModel { FileName= fileName, FilePath = fileName, isImage = false };
        }

        public async Task<BlobContentModel> ExportExamToWord(Exam exam)
        {
            var department = await _context.Departments.FindAsync(exam.DepartmentId.ToUpper());
            var subject = await _context.Subjects.FindAsync(exam.SubjectId.ToUpper());
            var teacherCreate = await _context.Users.FindAsync(exam.TeacherCreatedId);

            string status = "";
            if (exam.Status == Enums.Status.Unsubmitted)
            {
                status = "Chưa gửi phê duyệt";
            }
            else if (exam.Status == Enums.Status.PendingApproval)
            {
                status = "Đang chờ phê duyệt";
            }
            else if (exam.Status == Enums.Status.Approved)
            {
                status = "Đã phê duyệt";
            }
            else if (exam.Status == Enums.Status.RefuseApproval)
            {
                status = "Đã từ chối phê duyệt";
            }
            else if (exam.Status == Enums.Status.CancelApproval)
            {
                status = "Đã hủy phê duyệt";
            }
            else
            {
                status = "Lưu nháp";
            }

            List<string> value = new List<string>
            {
                $"{exam.Id}", $"{exam.FileName}","Tự luận", $"{exam.Duration} phút", $"{exam.ScoringScale}", $"{exam.DateCreated.ToString("dd/MM/yyyy")}",
                $"{department?.Name}",  $"{subject?.Name}", $"{exam.Note}",  $"{teacherCreate?.FullName}", $"{status}"
            };

            Document doc = new Document();
            // Use a document builder to add content to the document.
            DocumentBuilder builder = new DocumentBuilder(doc);

            // Set font formatting properties
            Aspose.Words.Font font = builder.Font;
            font.Bold = false;
            font.Color = System.Drawing.Color.Black;
            font.Name = "Times New Roman";
            font.Size = 12;

            // Mark the start of Word Table 
            Table table= builder.StartTable();

            for (int i = 0; i < title.Count(); i++)
            {
                builder.InsertCell();
                font.Bold = true;
                builder.Write($"{title[i]}:");
                builder.InsertCell();
                font.Bold = false;
                builder.Write($"{value[i]}");
                builder.EndRow();
            }
            builder.EndTable();

            builder.Writeln();

            //Add question
            List<Question_Exam> listQuestion = exam.Question_Exam.ToList();
            int questionCount = listQuestion.Count();

            for (int i = 0; i < questionCount; i++)
            {
                var settingQuestion = listQuestion[i].QuestionBanks.QB_Answer_Essay;

                font.Bold = true;

                string submitType;
                if (settingQuestion.SubmitType)
                {
                    submitType = "Tải tệp lên";
                }
                else
                {
                    submitType = "Điền đáp án trực tiếp vào câu trả lời";

                    if (settingQuestion.LimitWord != 0 || String.IsNullOrEmpty(settingQuestion.LimitWord.ToString()))
                    {
                        submitType += $" (giới hạn {settingQuestion.LimitWord} từ)";
                    }
                    else
                    {
                        submitType += " (không giới hạn số từ)";
                    }
                }

                builder.Writeln($"Câu {i + 1}: [{submitType}]");

                font.Bold = false;
                if (listQuestion[i].QuestionBanks.Content.EndsWith('.'))
                {
                    builder.Writeln("\t" + listQuestion[i].QuestionBanks.Content);
                }
                else
                {
                    builder.Writeln("\t" + listQuestion[i].QuestionBanks.Content+'.');
                }
                builder.Writeln();
            }

            string fileName = exam.Id+".docx";
            doc.Save(fileName, Aspose.Words.SaveFormat.Docx);

            return new BlobContentModel { FileName = fileName, FilePath = fileName, isImage = false };
        }
    }
}
