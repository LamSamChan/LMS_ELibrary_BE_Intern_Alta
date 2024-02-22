
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using static LMS_Library_API.Enums.NotificationFearture;

namespace LMS_Library_API.Models.Notification
{
    public class NotificationFeatures
    {
        [Key] public int Id { get; set; }

        [Required]
        public FeartureType FeatureType { get; set; }

        [Required]
        public NotificationType Type { get; set; }

        //navigation property
        [JsonIgnore]
        [InverseProperty("NotificationFeatures")]
        public virtual ICollection<NotificationSetting>? NotificationSetting { get; set; }
    }
}
