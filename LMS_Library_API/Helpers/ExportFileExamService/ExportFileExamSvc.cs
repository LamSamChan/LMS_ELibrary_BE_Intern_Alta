using Aspose.Cells;
using LMS_Library_API.Models.BlobStorage;
using LMS_Library_API.Models.Exams;

namespace LMS_Library_API.Helpers.ExportFileExamService
{
    public class ExportFileExamSvc : IExportFileExamSvc
    {
        public BlobContentModel ExportExamToExcel(Exam exam)
        {
            return new BlobContentModel { FileName= "", FilePath ="", isImage = false };
        }

        public BlobContentModel ExportExamToWord(Exam exam)
        {
            return new BlobContentModel { FileName = "", FilePath = "", isImage = false };
        }
    }
}
