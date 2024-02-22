using LMS_Library_API.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LMS_Library_API.ModelsDTO
{
    public class SubjectDTO
    {
        [MaxLength(20)]
        [Key] public string Id { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime SubmissionDate { get; set; } = DateTime.Now;

        [Required]
        public string Description { get; set; }


        //navigation property
        public string DepartmentId { get; set; }

        public Guid TeacherId { get; set; }
    }
}
