using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace LMS_Library_API.Models.Exams
{
    public class QB_Answer_Essay
    {
        [Key] public int  Id { get; set; }

        [Column(TypeName ="bit")]
        [Required]
        //true: tai tep len / false dien truc tiep
        public bool SubmitType { get; set; }

        public int? LimitWord { get; set; }

        //navigation property
        [ForeignKey("QuestionBanks")]
        public int QuestionId { get; set; }
        [JsonIgnore]
        public virtual QuestionBanks QuestionBanks { get; set; }
    }
}
