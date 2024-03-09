using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace LMS_Library_API.Models.Exams
{
    public class Question_Exam
    {
        [ForeignKey("Exam")]
        [Column(TypeName = "varchar(30)")]
        public string ExamId { get; set; }
        [JsonIgnore]
        public virtual Exam Exam { get; set; }

        [ForeignKey("QuestionBanks")]
        public int QuestionId { get; set; }
        public virtual QuestionBanks QuestionBanks { get; set; }
    }
}
