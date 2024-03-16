using LMS_Library_API.Models.AboutSubject;
using LMS_Library_API.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace LMS_Library_API.ModelsDTO
{
    public class NotificationClassStudentDTO
    {
        public int? SubjectNotificationId { get; set; }

        [Required]
        [MaxLength(30)]
        public string ClassId { get; set; }

        public Guid? StudentId { get; set; }

        [Required]
        public bool IsForAllStudent { get; set; }
    }
}
