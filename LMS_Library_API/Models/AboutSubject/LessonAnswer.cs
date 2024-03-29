﻿using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using LMS_Library_API.Models.AboutUser;
using LMS_Library_API.Models.AboutStudent;

namespace LMS_Library_API.Models.AboutSubject
{
    public class LessonAnswer
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName = "nvarchar(255)")]
        [MaxLength(255)]
        [Required]
        public string content { get; set; }

        [Required]
        public int likesCounter { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime createdAt { get; set; } = DateTime.Now;

        [Required]
        public bool isTeacher { get; set; }

        //navigation property

        [ForeignKey("LessonQuestion")]
        public int lessonQuestionId { get; set; }
        [JsonIgnore]
        public virtual LessonQuestion LessonQuestion { get; set; }

        [ForeignKey("User")]
        [AllowNull]
        public Guid? teacherId { get; set; }
        public virtual User? User { get; set; }

        [ForeignKey("Student")]
        [AllowNull]
        public Guid? studentId { get; set; }
        public virtual Student? Student { get; set; }


        [JsonIgnore]
        [InverseProperty("LessonAnswer")]
        public virtual ICollection<AnswerLike> AnswerLikes { get; set; }

        [JsonIgnore]
        [InverseProperty("LessonAnswer")]
        public virtual ICollection<StudentAnswerLike> StudentAnswerLikes { get; set; }
    }
}
