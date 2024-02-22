using LMS_Library_API.Enums;
using LMS_Library_API.Models.AboutSubject;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using LMS_Library_API.Models.BlobStorage;

namespace LMS_Library_API.ModelsDTO
{
    public class DocumentDTO
    {
        [Key] public int Id { get; set; }

        [MaxLength(255)]
        [Required]
        public string Name { get; set; }

        //0: tai lieu, 1: bai giang
        [Required]
        public bool Type { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime submissionDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime updatedDate { get; set; } = DateTime.Now;

        [Required]
        public BlobContentModel FilePath { get; set; }

        [Required]
        [DefaultValue(Status.PendingApproval)]
        public Status status { get; set; }

        [AllowNull]
        public string note { get; set; }

        //navigation property
        [Required]
        public int lessonId { get; set; }

        [AllowNull]
        public Guid? censorId { get; set; }

        [Required]
        public Guid teacherCreatedId { get; set; }
    }
}
