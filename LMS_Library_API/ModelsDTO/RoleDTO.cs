using LMS_Library_API.Models.RoleAccess;
using LMS_Library_API.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.ComponentModel;

namespace LMS_Library_API.ModelsDTO
{
    public class RoleDTO
    {
        [Key] public int Id { get; set; }

        [Column(TypeName = "nvarchar(30)")]
        [MaxLength(30)]
        [Required]
        public string Name { get; set; }

        [Column(TypeName = "nvarchar(255)")]
        [AllowNull]
        public string Description { get; set; }

        public DateTime DateUpdated { get; set; } = DateTime.Now;

        public virtual ICollection<Role_PermissionsDTO> Role_Permissions { get; set; }
    }
}
