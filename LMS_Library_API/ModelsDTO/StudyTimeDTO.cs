using LMS_Library_API.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace LMS_Library_API.ModelsDTO
{
    public class StudyTimeDTO
    {
        [Required]
        public Guid studentId { get; set; }

        [Required]
        [MaxLength(20)]
        public string subjectId { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime studyDate { get; set; } = DateTime.Now;

        [Required]
        [DefaultValueAttribute(0)]
        public int totalTime { get; set; }
    }
}
