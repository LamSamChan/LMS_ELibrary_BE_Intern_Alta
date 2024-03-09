using LMS_Library_API.Models.AboutSubject;
using LMS_Library_API.Models.Exams;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS_Library_API.Models.AboutUser
{
    public class AnswerLike
    {
        [ForeignKey("User")]
        public Guid UserId { get; set; }
        public virtual User User { get; set; }

        [ForeignKey("Lesson")]
        public int LessonId { get; set; }
        public virtual Lesson Lesson { get; set; }

        [ForeignKey("LessonAnswer")]
        public int LessonAnswerId { get; set; }
        public virtual LessonAnswer LessonAnswer { get; set; }
    }
}
