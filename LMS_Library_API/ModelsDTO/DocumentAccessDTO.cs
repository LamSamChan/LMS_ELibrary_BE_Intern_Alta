using System.ComponentModel.DataAnnotations;

namespace LMS_Library_API.ModelsDTO
{
    public class DocumentAccessDTO
    {
        [Required]
        public int documentId { get; set; }

        public string? classId { get; set; }

        [Required]
        public bool isForAllClasses { get; set; }
    }
}
