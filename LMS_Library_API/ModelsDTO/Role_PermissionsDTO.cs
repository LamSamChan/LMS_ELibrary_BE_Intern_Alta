using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace LMS_Library_API.ModelsDTO
{
    public class Role_PermissionsDTO
    {
        [Required]
        public int RoleId { get; set; }

        [Required]
        public int PermissionsId { get; set; }

        [Required]
        [DefaultValue(false)]
        public bool CanAccess { get; set; } = false;

    }
}
