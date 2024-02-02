using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS_Library_API.Models
{
    public class Department
    {
        [Key]
        [Column(TypeName ="varchar(20)")]
        public string Id { get; set; }

        [Column(TypeName ="nvarchar(150)")]
        [Required]
        public string Name { get; set; }


        //navigation property
        public virtual User User { get; set; }

    }
}
