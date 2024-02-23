using LMS_Library_API.Enums;
using LMS_Library_API.Models.Exams;
using LMS_Library_API.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LMS_Library_API.ModelsDTO
{
    public class QuestionBankES_DTO
    {
        [Key] public int Id { get; set; }

        [Required]
        public bool Format { get; set; }

        [Column(TypeName = "nvarchar")]
        [Required]
        public string Content { get; set; }

        [Required]
        public Level Level { get; set; }

        public DateTime LastUpdated { get; set; }

        //navigation property
        [Required]
        public Guid TeacherCreatedId { get; set; }

        [Required]
        public string SubjectId { get; set; }

        public virtual QB_Answer_ES_DTO QB_Answer_Essay { get; set; }
    }
}
