using LMS_Library_API.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS_Library_API.ModelsDTO
{
    public class CustomInfoOfSubjectDTO
    {
        
            [Key] public int Id { get; set; }

            [Required]
            public string title { get; set; }


            [Required]
            public string content { get; set; }

            [Required]
            public Status status { get; set; }

            public string subjectId { get; set; }
        
    }
}
