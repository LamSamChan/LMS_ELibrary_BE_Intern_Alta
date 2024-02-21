using LMS_Library_API.Models.Notification;
using LMS_Library_API.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LMS_Library_API.ModelsDTO
{
    public class NotificationSettingDTO
    {
        [ForeignKey("User")]
        public Guid UserId { get; set; }

        [ForeignKey("NotificationFeatures")]
        public int FeaturesId { get; set; }

        [Required]
        public bool ReceiveNotification { get; set; }

    }
}
