using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace LMS_Library_API.Enums
{
    public enum Gender
    {
        [Display(Name = "Nam")]
        Male = 1,
        [Display(Name = "Nữ")]
        Female = 2
    }
}
