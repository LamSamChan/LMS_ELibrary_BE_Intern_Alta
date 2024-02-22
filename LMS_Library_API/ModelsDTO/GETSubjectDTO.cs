using LMS_Library_API.Enums;
using LMS_Library_API.Models;
using System.ComponentModel.DataAnnotations;

namespace LMS_Library_API.ModelsDTO
{
    public class GETSubjectDTO
    {
        [MaxLength(20)]
        [Key] public string Id { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime SubmissionDate { get; set; } = DateTime.Now;

        [Required]
        public string Description { get; set; }

        public int CountDocument { get; set; }

        public int CountDocumentApproved { get; set; }

        public User Teacher { get; set; }
    }
}
