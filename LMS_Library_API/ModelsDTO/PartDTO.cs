using LMS_Library_API.Enums;
using LMS_Library_API.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.ComponentModel;

namespace LMS_Library_API.ModelsDTO
{
    public class PartDTO
    {
        public int Id { get; set; }

        public string name { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime dateSubmited { get; set; } = DateTime.Now;

        [DefaultValue(false)]
        public bool isHidden { get; set; }

        [Required]
        public int numericalOrder { get; set; }

        [Required]
        [DefaultValue(Status.PendingApproval)]
        public Status status { get; set; }

        [AllowNull]
        public string? note { get; set; }

        //navigation property

        [AllowNull]
        public Guid? censorId { get; set; }

        [Required]
        public Guid teacherCreatedId { get; set; }

        [Required]
        public string subjectId { get; set; }

    }
}
