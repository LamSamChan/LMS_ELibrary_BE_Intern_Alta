﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS_Library_API.Models.StudentNotification
{
    public class StudentNotificationSetting
    {
        [ForeignKey("Student")]
        public Guid studentId { get; set; }

        [ForeignKey("StudentNotificationFeatures")]
        public int featuresId { get; set; }

        [Required]
        public bool receiveNotification { get; set; }

        // Navigation property
        public virtual Student Student { get; set; }

        public virtual StudentNotificationFeatures StudentNotificationFeatures { get; set; }
    }
}
