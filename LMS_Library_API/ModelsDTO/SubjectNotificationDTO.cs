using LMS_Library_API.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using LMS_Library_API.Models.AboutSubject;

namespace LMS_Library_API.ModelsDTO
{
    public class SubjectNotificationDTO
    {
         public int Id { get; set; }

        [MaxLength(100)]
        [Required]
        public int title { get; set; }

        [MaxLength(100)]
        [Required]
        public int content { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        //navigation property
        [Required]
        public string subjectId { get; set; }

        [Required]
        public Guid teacherId { get; set; }

        public virtual ICollection<NotificationClassStudentDTO> NotificationClassStudents { get; set; }
    }
}
