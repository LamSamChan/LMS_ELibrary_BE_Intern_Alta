using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace LMS_Library_API.ModelsDTO
{
    public class QB_Answer_ES_DTO
    {
        [Key] public int Id { get; set; }

        [Column(TypeName = "bit")]
        [Required]
        public bool SubmitType { get; set; }

        [AllowNull]
        public int? LimitWord { get; set; }

        //navigation property
        [ForeignKey("QuestionBanks")]
        public int QuestionId { get; set; }
    }
}
