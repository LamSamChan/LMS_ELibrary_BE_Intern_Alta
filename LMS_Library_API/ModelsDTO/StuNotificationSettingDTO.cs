using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LMS_Library_API.ModelsDTO
{
    public class StuNotificationSettingDTO
    {
        [Required]
        public Guid studentId { get; set; }

        [Required]
        public int featuresId { get; set; }

        [Required]
        public bool receiveNotification { get; set; }
    }
}
