using LMS_Library_API.Models.AboutSubject;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace LMS_Library_API.Models.AboutStudent
{
    public class StudyHistory
    {
        [ForeignKey("Student")]
        public Guid StudentId { get; set; }
        [JsonIgnore]
        public virtual Student Student { get; set; }

        [ForeignKey("Lesson")]
        public int LessonId { get; set; }
        public virtual Lesson Lesson { get; set; }

        [Required]
        public int WatchMinutes { get; set; }

        [Required]
        public DateTime DateUpdate { get; set; } = DateTime.Now;
    }
}
