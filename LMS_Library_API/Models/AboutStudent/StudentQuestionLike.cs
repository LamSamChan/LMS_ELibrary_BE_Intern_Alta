using LMS_Library_API.Models.AboutSubject;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS_Library_API.Models.AboutStudent
{
    public class StudentQuestionLike
    {
        [ForeignKey("Student")]
        public Guid StudentId { get; set; }
        public virtual Student Student { get; set; }

        [ForeignKey("Lesson")]
        public int LessonId { get; set; }
        public virtual Lesson Lesson { get; set; }

        [ForeignKey("LessonQuestion")]
        public int LessonQuestionId { get; set; }
        public virtual LessonQuestion LessonQuestion { get; set; }
    }
}
