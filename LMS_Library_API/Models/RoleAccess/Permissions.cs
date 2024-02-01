﻿using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LMS_Library_API.Models.RoleAccess
{
    public class Permissions
    {
        [Key] public int Id { get; set; }

        [Column(TypeName = "nvarchar(255)")]
        [Required]
        public string Name { get; set; }

        //navigation property
        [InverseProperty("Permissions")]
        public virtual ICollection<Role_Permissions> Role_Permissions { get; set; }

    }
}
