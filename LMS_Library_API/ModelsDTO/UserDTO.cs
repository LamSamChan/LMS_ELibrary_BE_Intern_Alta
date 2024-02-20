﻿using LMS_Library_API.Enums;
using LMS_Library_API.Models.RoleAccess;
using LMS_Library_API.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.ComponentModel;
using LMS_Library_API.Models.BlobStorage;

namespace LMS_Library_API.ModelsDTO
{
    public class UserDTO
    {
        public Guid Id { get; set; } = new Guid();

        [Column(TypeName = "nvarchar(50)")]
        [StringLength(50, ErrorMessage = "Vượt quá độ dài cho phép")]
        [Required(ErrorMessage = "Hãy nhập tên người dùng")]
        public string FullName { get; set; }

        [Column(TypeName = "varchar(50)")]
        [StringLength(50, ErrorMessage = "Vượt quá độ dài cho phép")]
        [Required(ErrorMessage = "Hãy nhâp email người dùng")]
        [RegularExpression("^[a-zA-Z0-9]+(?:\\.[a-zA-Z0-9]+)*@[a-zA-Z0-9]+(?:\\.[a-zA-Z0-9]+)*$",
            ErrorMessage = "Email không hợp lệ !")]
        public string Email { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Required(ErrorMessage = "Hãy nhập ngày sinh người dùng")]
        public DateTime DateOfBirth { get; set; }

        [Column(TypeName = "varchar(20)")]
        [StringLength(20, ErrorMessage = "Vượt quá độ dài cho phép")]
        [Required(ErrorMessage = "Hãy nhập số điện thoại người dùng")]
        [RegularExpression("^(?:\\+84|0)\\d{9}$", ErrorMessage = "Số điện thoại không hợp lệ !")]
        public string PhoneNumber { get; set; }

        [Column(TypeName = "nvarchar(150)")]
        [StringLength(150, ErrorMessage = "Vượt quá độ dài cho phép")]
        [Required(ErrorMessage = "Hãy nhập địa chỉ nhà của người dùng")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Hãy lựa chọn giới tính của người dùng")]
        [Range(1, 2, ErrorMessage = "Giới tính người dùng không hợp lệ")]
        public Gender Gender { get; set; }

        [AllowNull]
        public BlobContentModel Avartar { get; set; } = new BlobContentModel();
        public string? Password { get; set; }

        [DefaultValue(false)]
        [Required]
        public bool isLocked { get; set; }

        //navigation property
        [ForeignKey("Role")]
        public int RoleId { get; set; }

        [ForeignKey("Department")]
        [Column(TypeName = "varchar(20)")]
        public string DepartmentId { get; set; }
    }
}
