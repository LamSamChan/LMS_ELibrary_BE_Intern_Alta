using LMS_Library_API.Enums;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using LMS_Library_API.Models.Exams;

namespace LMS_Library_API.ModelsDTO
{
    public class Essay_QuestionBankDTO
    {
        [Required]
        [DefaultValue(false)]
        public bool Format { get; set; }

        [Column(TypeName = "nvarchar(max)")]
        [Required]
        public string Content { get; set; }

        [Required]
        public Level Level { get; set; }

        public DateTime LastUpdated { get; set; } = DateTime.Now;

        //navigation property

        [Required]
        public Guid TeacherCreatedId { get; set; }

        [Required]
        public string SubjectId { get; set; }

        public virtual QB_Answer_ES_DTO QB_Answer_Essay { get; set; }
    }
}
