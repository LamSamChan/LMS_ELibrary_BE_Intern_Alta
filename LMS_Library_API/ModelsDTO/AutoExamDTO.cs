using LMS_Library_API.Enums;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace LMS_Library_API.ModelsDTO
{
    public class AutoExamDTO
    {
        [Required]
        public string FileName { get; set; }


        [Required]
        public int ScoringScale { get; set; }

        [Required]
        public DateTime DateCreated { get; set; } = DateTime.Now;


        [Required]
        public Guid TeacherCreatedId { get; set; }


        [Required]
        public string SubjectId { get; set; }

        [Required]
        public string DepartmentId { get; set; }
    }
}
