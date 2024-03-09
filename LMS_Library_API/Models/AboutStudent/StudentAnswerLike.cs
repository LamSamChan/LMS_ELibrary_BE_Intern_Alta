using LMS_Library_API.Models.AboutSubject;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS_Library_API.Models.AboutStudent
{
    public class StudentAnswerLike
    {
        [ForeignKey("Student")]
        public Guid StudentId { get; set; }
        public virtual Student Student { get; set; }

        [ForeignKey("Lesson")]
        public int LessonId { get; set; }
        public virtual Lesson Lesson { get; set; }

        [ForeignKey("LessonAnswer")]
        public int LessonAnswerId { get; set; }
        public virtual LessonAnswer LessonAnswer { get; set; }
    }
}
