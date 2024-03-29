﻿using LMS_Library_API.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace LMS_Library_API.Models.AboutSubject
{
    public class CustomInfoOfSubject
    {
        [Key] public int Id { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        [Required]
        public string title { get; set; }


        [Column(TypeName = "nvarchar(255)")]
        [Required]
        public string content { get; set; }

        [Required]
        public Status status { get; set; }

        //navigation property
        [ForeignKey("Subject")]
        public string subjectId { get; set; }
        [JsonIgnore]
        public virtual Subject Subject { get; set; }
    } 
}
