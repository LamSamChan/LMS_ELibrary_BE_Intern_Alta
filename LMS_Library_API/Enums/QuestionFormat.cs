using Newtonsoft.Json.Linq;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Xml.Linq;

namespace LMS_Library_API.Enums
{
    public enum QuestionFormat
    {
        [Display(Name = "Trắc nghiệm")]
        MultipleChoice = 0,
        [Display(Name = "Tự luận")]
        Essay = 1
    }
}
