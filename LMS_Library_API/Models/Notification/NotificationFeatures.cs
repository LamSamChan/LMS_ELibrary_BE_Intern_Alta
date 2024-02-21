
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace LMS_Library_API.Models.Notification
{
    public class NotificationFeatures
    {
        [Key] public int Id { get; set; }

        [Column(TypeName ="nvarchar(50)")]
        [Required]
        public string FeatureType { get; set; }

        [Column(TypeName = "nvarchar(30)")]
        [Required]
        public string Type { get; set; }

        //navigation property
        [JsonIgnore]
        [InverseProperty("NotificationFeatures")]
        public virtual ICollection<NotificationSetting> NotificationSetting { get; set; }
    }
}
