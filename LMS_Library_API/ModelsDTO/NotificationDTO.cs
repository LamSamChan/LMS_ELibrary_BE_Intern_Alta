using Newtonsoft.Json;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS_Library_API.ModelsDTO
{
    public class NotificationDTO
    {
        [Key] public int Id { get; set; }

        [Column(TypeName = "nvarchar(255)")]
        [MaxLength(255)]
        [Required]
        public string Content { get; set; }

        [Required]
        [DefaultValue(false)]
        public bool IsRead { get; set; }

        [DefaultValue(false)]
        [Required]
        public bool IsTeacherSend { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Required]
        public DateTime TimeCounter { get; set; }

        //navigation property
        public Guid? RecipientId { get; set; }
        public Guid? SenderId { get; set; }
        public Guid? StudentRecipientId { get; set; }
        public Guid? StudentSenderId { get; set; }

    }
}
