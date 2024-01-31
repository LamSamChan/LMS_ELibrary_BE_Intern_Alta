using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS_Library_API.Models.AboutUser
{
    public class Help
    {
        [Key] public int Id { get; set; }

        [Column(TypeName ="nvarchar")]
        [Required]
        public string Content { get; set; }

        //navigation property
        [ForeignKey("User")]
        public Guid UserId { get; set; }
        public virtual User User { get; set; }
    }
}
