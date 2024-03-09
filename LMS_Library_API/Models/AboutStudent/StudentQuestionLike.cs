using LMS_Library_API.Models.AboutSubject;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace LMS_Library_API.Models.AboutStudent
{
    public class StudentQuestionLike
    {
        [ForeignKey("Student")]
        public Guid StudentId { get; set; }
        [JsonIgnore]
        public virtual Student Student { get; set; }

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
