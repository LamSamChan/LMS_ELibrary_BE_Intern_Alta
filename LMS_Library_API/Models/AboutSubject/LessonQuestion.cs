using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS_Library_API.Models.AboutSubject
{
    public class LessonQuestion
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName ="nvarchar(255)")]
        [MaxLength(255)]
        [Required]
        public string content { get; set; }

        [Required]
        public int likesCounter { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime createdAt { get; set; } = DateTime.Now;

        [Required]
        public bool isTeacher { get; set; }

        //navigation property

        [ForeignKey("Lesson")]
        public int lessonId { get; set; }
        public virtual Lesson Lesson { get; set; }

        [ForeignKey("User")]
        public Guid userId { get; set; }
        public virtual User User { get; set; }


        public virtual ICollection<LessonAnswer> LessonAnswers { get; set; }


    }
}
