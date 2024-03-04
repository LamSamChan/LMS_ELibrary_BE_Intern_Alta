using LMS_Library_API.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LMS_Library_API.ModelsDTO
{
    public class StudyHistoryDTO
    {
        [Required]
        public Guid studentId { get; set; }

        [Required]
        public int documentId { get; set; }

        [Required]
        public int watchMinutes { get; set; }

        [Required]
        public DateTime dateUpdate { get; set; } = DateTime.Now;
    }
}
