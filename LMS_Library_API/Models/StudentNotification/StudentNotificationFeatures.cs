
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace LMS_Library_API.Models.StudentNotification
{
    public class StudentNotificationFeatures
    {
        [Key] public int Id { get; set; }

        [Column(TypeName ="nvarchar(50)")]
        [Required]
        public string FeatureType { get; set; }

        [Required]
        public string Type { get; set; }

        //navigation property
        [JsonIgnore]
        [InverseProperty("StudentNotificationFeatures")]
        public virtual ICollection<StudentNotificationSetting> StudentNotificationSetting { get; set; }
    }
}
