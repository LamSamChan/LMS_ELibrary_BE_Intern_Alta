
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS_Library_API.Models.Notification
{
    public class NotificationFeatures
    {
        [Key] public int Id { get; set; }

        [Column(TypeName ="nvarchar(50)")]
        [Required]
        public string FeatureType { get; set; }

        //navigation property
        [InverseProperty("NotificationFeatures")]
        public virtual ICollection<NotificationSetting> NotificationSetting { get; set; }
    }
}
