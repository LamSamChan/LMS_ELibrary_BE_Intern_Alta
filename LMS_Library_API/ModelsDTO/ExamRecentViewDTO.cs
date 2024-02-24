using LMS_Library_API.Models.Exams;
using LMS_Library_API.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LMS_Library_API.ModelsDTO
{
    public class ExamRecentViewDTO
    {
        [Required]
        public Guid UserId { get; set; }
        [Required]
        public string ExamId { get; set; }
    }
}
