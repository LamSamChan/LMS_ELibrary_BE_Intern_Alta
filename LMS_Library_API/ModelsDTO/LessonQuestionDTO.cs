using LMS_Library_API.Models.AboutSubject;
using LMS_Library_API.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace LMS_Library_API.ModelsDTO
{
    public class LessonQuestionDTO
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
        public int lessonId { get; set; }

        public Guid? teacherId { get; set; }

        public Guid? studentId { get; set; }
    }
}
