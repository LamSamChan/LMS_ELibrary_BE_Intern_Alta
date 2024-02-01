using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace LMS_Library_API.Enums
{
    public enum Level
    {
        [Display(Name = "Dễ")]
        Easy = 1,
        [Display(Name = "Trung bình")]
        Medium = 2,
        [Display(Name = "Khó")]
        Hard = 3
    }
}
