﻿using LMS_Library_API.Enums;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace LMS_Library_API.Models.Exams
{
    public class Exam
    {
        [Key] public string Id { get; set; }

        [Column(TypeName = "varchar(30)")]
        [Required]
        public string FileType { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        [StringLength(100, ErrorMessage = "Vượt quá độ dài cho phép")]
        [Required]
        public string FileName { get; set; }

        [Required]
        public int Duration { get; set; }

        [Required]
        public DateTime DateCreated { get; set; } = DateTime.Now;

        [Column(TypeName = "varchar")]
        [Required]
        public string FilePath {get; set; }

        [Required]
        public Status Status { get; set; }

        [Column(TypeName = "nvarchar(255)")]
        [MaxLength(255)]
        [AllowNull]  
        public string Note { get; set; }


        //navigation property
        [ForeignKey("User")]
        [AllowNull]
        public Guid Censor { get; set; }
        [ForeignKey("User")]
        public Guid TeacherCreate { get; set; }
        public virtual User User { get; set; }

    }
}
