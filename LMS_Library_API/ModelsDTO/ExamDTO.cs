using LMS_Library_API.Enums;
using LMS_Library_API.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using LMS_Library_API.Models.BlobStorage;

namespace LMS_Library_API.ModelsDTO
{
    public class ExamDTO
    {
        [Key] public string Id { get; set; }

        [Required]
        public string FileType { get; set; }

        [Required]
        public string FileName { get; set; }

        //false: tu luan, true: trac nghiem
        [Required]
        public bool Format { get; set; }

        [Required]
        public int Duration { get; set; }

        [Required]
        public DateTime DateCreated { get; set; } = DateTime.Now;

        [Required]
        public BlobContentModel FilePath { get; set; }

        [Required]
        public Status Status { get; set; }

        [AllowNull]
        public string Note { get; set; }


        //navigation property
        [AllowNull]
        public Guid CensorId { get; set; }

        [Required]
        public Guid TeacherCreatedId { get; set; }


        [Required]
        public string SubjectId { get; set; }
    }
}
