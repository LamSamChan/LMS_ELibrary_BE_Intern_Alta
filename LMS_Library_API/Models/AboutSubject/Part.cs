﻿using LMS_Library_API.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace LMS_Library_API.Models.AboutSubject
{
    public class Part
    {
        [Key] public int Id { get; set; }

        [Column(TypeName = "nvarchar(255)")]
        [Required]
        public string name { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime dateSubmited { get; set; } = DateTime.Now;

        public bool isHidden { get; set; } = false;

        [Required]        
        public int numericalOrder { get; set; }

        [Required]
        public Status status { get; set; }

        [Column(TypeName ="nvarchar(255)")]
        [AllowNull]
        public string? note { get; set; }

        //navigation property

        [ForeignKey("Censor")]
        public Guid? censorId { get; set; }
        public virtual User? Censor { get; set; }


        [ForeignKey("TeacherCreated")]
        [Required]
        public Guid teacherCreatedId { get; set; }
        public virtual User TeacherCreated { get; set; }

        [ForeignKey("Subject")]
        public string subjectId { get; set; }
        [JsonIgnore]
        public virtual Subject Subject { get; set; }

        public virtual ICollection<Lesson> Lessons { get; set; }
    }
}
