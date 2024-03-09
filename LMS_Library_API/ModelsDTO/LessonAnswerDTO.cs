using System.ComponentModel.DataAnnotations;

namespace LMS_Library_API.ModelsDTO
{
    public class LessonAnswerDTO
    {
        [MaxLength(255)]
        [Required]
        public string content { get; set; }

        [Required]
        public int likesCounter { get; set; } = 0;
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime createdAt { get; set; } = DateTime.Now;

        //navigation property

        [Required]
        public int lessonQuestionId { get; set; }

        public Guid? teacherId { get; set; }

        public Guid? studentId { get; set; }
    }
}
