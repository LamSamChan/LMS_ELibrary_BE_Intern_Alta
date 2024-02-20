using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using LMS_Library_API.Models.BlobStorage;

namespace LMS_Library_API.ModelsDTO
{
    public class PrivateFileDTO
    {
        public int Id { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string Modifier { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DateChanged { get; set; } = DateTime.Now;

        [Required]
        public BlobContentModel BlobContent { get; set; }
     
        public Guid UserId { get; set; }
    }
}
