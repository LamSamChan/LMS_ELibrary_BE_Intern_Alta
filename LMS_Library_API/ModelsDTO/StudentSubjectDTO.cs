using LMS_Library_API.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LMS_Library_API.ModelsDTO
{
    public class StudentSubjectDTO
    {
        [Required]
        public Guid studentId { get; set; }

        [Required]
        public string subjectId { get; set; }

        [Required]
        public bool subjectMark { get; set; }
    }
}
