using LMS_Library_API.Enums;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace LMS_Library_API.ModelsDTO
{
    public class QuestionBankMC_DTO
    {
        [Key] public int Id { get; set; }

        [Required]
        [DefaultValue(true)]
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

        public virtual ICollection<QB_Answer_MC_DTO> QB_Answers_MC { get; set; }
    }
}
