﻿using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
namespace LMS_Library_API.Enums
{
    public enum Languages
    {
        [Display(Name = "Tiếng Việt")]
        [EnumMember(Value = "vi")]
        Vietnamese,
        [Display(Name = "English")]
        [EnumMember(Value = "en")]
        English

    }
}
