using LMS_Library_API.Enums;
using LMS_Library_API.Models.AboutUser;
using LMS_Library_API.Models.Exams;
using LMS_Library_API.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace LMS_Library_API.ModelsDTO
{
    public class MC_ExamDTO
    {
        [Required]
        public string Id { get; set; }

        [StringLength(100, ErrorMessage = "Vượt quá độ dài cho phép")]
        [Required]
        public string FileName { get; set; }
        //false: tu luan, true: trac nghiem
        [Required]
        public bool Format { get; set; }

        [Required]
        public int Duration { get; set; }

        [Required]
        public int ScoringScale { get; set; }

        [Required]
        public DateTime DateCreated { get; set; } = DateTime.Now;

        [Required]
        public Status Status { get; set; }

        [MaxLength(255)]
        [AllowNull]
        public string? Note { get; set; }


        //navigation property
        public Guid CensorId { get; set; }

        [Required]
        public Guid TeacherCreatedId { get; set; }

        [Required]
        public string SubjectId { get; set; }

        [Required]
        public string DepartmentId { get; set; }

        public virtual ICollection<MC_QuestionExamDTO> Question_Exam { get; set; }

    }
}
