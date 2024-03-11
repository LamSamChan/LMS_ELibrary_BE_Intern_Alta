using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace LMS_Library_API.ModelsDTO
{
    public class QB_Answer_ES_DTO
    {
        [Required]
        public bool SubmitType { get; set; }

        public int? LimitWord { get; set; }
    }
}
