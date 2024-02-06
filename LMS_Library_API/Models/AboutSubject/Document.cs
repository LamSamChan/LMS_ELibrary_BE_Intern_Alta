using LMS_Library_API.Enums;
using LMS_Library_API.Models.AboutStudent;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Reflection.Metadata.Ecma335;

namespace LMS_Library_API.Models.AboutSubject
{
    public class Document
    {
        [Key] public int Id { get; set; }

        [Column(TypeName ="nvarchar(255)")]
        [MaxLength(255)]
        [Required]
        public string Name { get; set; }

        //0: tai lieu, 1: bai giang
        [Required]
        public bool Type { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime submissionDate { get; set; } = DateTime.Now;

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime updatedDate { get; set; } = DateTime.Now;

        [Column(TypeName = "nvarchar(255)")]
        [MaxLength(255)] 
        [Required]
        public string FilePath { get; set; }

        [Required]
        public Status status { get; set; }

        [Column(TypeName ="nvarchar(255)")]
        [AllowNull]
        public string note { get; set; }

        //navigation property
        [ForeignKey("Lesson")]
        public int lessonId { get; set; }
        public virtual Lesson Lesson { get; set; }

        [ForeignKey("User")]
        [AllowNull]
        public Guid censorId { get; set; }
        public virtual User User { get; set; }

        [InverseProperty("Document")]
        public virtual ICollection<StudyHistory> StudyHistories { get; set; }


    }
}
