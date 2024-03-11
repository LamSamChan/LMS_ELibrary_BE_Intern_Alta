using LMS_Library_API.Models.BlobStorage;
using LMS_Library_API.Models.Exams;

namespace LMS_Library_API.Helpers.ExportFileExamService
{
    public interface IExportFileExamSvc
    {
        public BlobContentModel ExportExamToExcel(Exam exam);
        public BlobContentModel ExportExamToWord(Exam exam);
    }
}
