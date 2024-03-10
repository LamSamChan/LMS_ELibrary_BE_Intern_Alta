using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LMS_Library_API.ModelsDTO
{
    public class QB_Answer_MC_DTO
    {
        [MaxLength(255)]
        [Required]
        public string AnswerContent { get; set; }

        [Required]
        public bool IsCorrect { get; set; }
    }
}
