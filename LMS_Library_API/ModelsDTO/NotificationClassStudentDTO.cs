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
        [Required]
        public int SubjectNotificationId { get; set; }

        [Required]
        [MaxLength(30)]
        public string ClassId { get; set; }

        [AllowNull]
        public Guid? StudentId { get; set; }
    }
}
