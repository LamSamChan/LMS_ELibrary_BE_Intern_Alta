using LMS_Library_API.Models.Exams;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS_Library_API.ModelsDTO
{
    public class Question_ExamDTO
    {
        [Required]
        public string ExamId { get; set; }
        [Required]
        public int QuestionId { get; set; }
    }
}
