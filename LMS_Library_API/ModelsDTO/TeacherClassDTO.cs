using LMS_Library_API.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LMS_Library_API.ModelsDTO
{
    public class TeacherClassDTO
    {
        [Required]
        public Guid teacherId { get; set; }
        [Required]
        public string classId { get; set; }
    }
}
