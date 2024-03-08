using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace LMS_Library_API.Models.AboutSubject
{
    public class NotificationClassStudent
    {
        [ForeignKey("SubjectNotification")]
        public int SubjectNotificationId { get; set; }
        public virtual SubjectNotification SubjectNotification { get; set; }

        [ForeignKey("Class")]
        [Column(TypeName = "nvarchar(30)")]
        [MaxLength(30)]
        public string ClassId { get; set; }
        [JsonIgnore]
        public virtual Class Class { get; set; }

        
        [ForeignKey("Student")]
        public Guid? StudentId { get; set; }
        [JsonIgnore]
        public virtual Student Student { get; set; }

        [Required]
        public bool IsForAllStudent { get; set; }
    }
}
