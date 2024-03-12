using LMS_Library_API.Models.BlobStorage;
using LMS_Library_API.Models.Exams;

namespace LMS_Library_API.Helpers.ExportFileExamService
{
    public interface IExportFileExamSvc
    {
        Task<BlobContentModel> ExportExamToExcel(Exam exam);
        Task<BlobContentModel> ExportExamToWord(Exam exam);
    }
}
