using LMS_Library_API.Models.Exams;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS_Library_API.Models.AboutUser
{
    public class ExamRecentViews
    {

        [ForeignKey("User")]
        public string UserId { get; set; }
        public virtual User User { get; set; }

        [ForeignKey("Exam")]
        public int ExamId { get; set; }
        public virtual Exam Exam { get; set; }
    }
}
