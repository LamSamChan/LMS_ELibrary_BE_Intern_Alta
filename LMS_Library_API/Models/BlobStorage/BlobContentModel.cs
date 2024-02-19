using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace LMS_Library_API.Models.BlobStorage
{
    public class BlobContentModel
    {
        [Required]
        public string FilePath { get; set; }
        [Required]
        public string FileName { get; set; }
        [Required]
        [DefaultValue(true)]
        public bool isImage { get; set; }
    }
}
