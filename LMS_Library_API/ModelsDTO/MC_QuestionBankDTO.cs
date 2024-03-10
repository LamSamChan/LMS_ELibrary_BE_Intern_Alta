using LMS_Library_API.Enums;
using LMS_Library_API.Models.Exams;
using LMS_Library_API.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace LMS_Library_API.ModelsDTO
{
    public class MC_QuestionBankDTO
    {
        [Required]
        [DefaultValue(true)]
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

        public virtual ICollection<QB_Answer_MC_DTO> QB_Answers_MC { get; set; }


    }
}
