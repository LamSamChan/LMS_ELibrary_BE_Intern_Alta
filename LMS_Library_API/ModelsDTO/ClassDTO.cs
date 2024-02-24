using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LMS_Library_API.ModelsDTO
{
    public class ClassDTO
    {
        [Required]
        public string Id { get; set; }

        [Required]
        public string name { get; set; }
    }
}
