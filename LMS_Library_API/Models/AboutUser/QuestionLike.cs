using LMS_Library_API.Models.AboutSubject;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace LMS_Library_API.Models.AboutUser
{
    public class QuestionLike
    {
        [ForeignKey("User")]
        public Guid UserId { get; set; }
        [JsonIgnore]
        public virtual User User { get; set; }

        [ForeignKey("Lesson")]
        public int LessonId { get; set; }
        [JsonIgnore]
        public virtual Lesson Lesson { get; set; }

        [ForeignKey("LessonQuestion")]
        public int LessonQuestionId { get; set; }
        [JsonIgnore]
        public virtual LessonQuestion LessonQuestion { get; set; }
    }
}
