using LMS_Library_API.Models.AboutSubject;
using LMS_Library_API.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LMS_Library_API.ModelsDTO
{
    public class LessonAccessDTO
    {
        [Required]
        public int lessonId { get; set; }

        public string? classId { get; set; }

        [Required]
        public bool isForAllClasses { get; set; }
    }
}
