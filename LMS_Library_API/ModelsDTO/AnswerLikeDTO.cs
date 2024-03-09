using LMS_Library_API.Models.AboutSubject;
using LMS_Library_API.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LMS_Library_API.ModelsDTO
{
    public class AnswerLikeDTO
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        public int LessonId { get; set; }

        [Required]
        public int LessonAnswerId { get; set; }

    }
}
