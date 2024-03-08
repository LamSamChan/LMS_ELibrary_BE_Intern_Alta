using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace LMS_Library_API.Models.AboutStudent
{
    public class StudentQnALikes
    {
        [Key]
        [ForeignKey("Student")]
        public Guid studentId { get; set; }

        [Column(TypeName ="varchar(max)")]
        [AllowNull]
        public string QuestionsLikedID { get; set; }

        [Column(TypeName ="varchar(max)")]
        [AllowNull]
        public string AnswersLikedID { get; set; }

        //navigation property
        [JsonIgnore]
        public virtual Student Student { get; set; }
    }
}
