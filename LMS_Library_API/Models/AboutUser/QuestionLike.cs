using LMS_Library_API.Models.AboutSubject;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS_Library_API.Models.AboutUser
{
    public class QuestionLike
    {
        [ForeignKey("User")]
        public Guid UserId { get; set; }
        public virtual User User { get; set; }

        [ForeignKey("Lesson")]
        public int LessonId { get; set; }
        public virtual Lesson Lesson { get; set; }

        [ForeignKey("LessonQuestion")]
        public int LessonQuestionId { get; set; }
        public virtual LessonQuestion LessonQuestion { get; set; }
    }
}
