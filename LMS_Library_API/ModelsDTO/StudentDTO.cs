using LMS_Library_API.Enums;
using LMS_Library_API.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using LMS_Library_API.Models.BlobStorage;

namespace LMS_Library_API.ModelsDTO
{
    public class StudentDTO
    {
        public Guid Id { get; set; } = new Guid();

        [StringLength(50, ErrorMessage = "Vượt quá độ dài cho phép")]
        [Required(ErrorMessage = "Hãy nhập tên người dùng")]
        public string FullName { get; set; }

        [StringLength(50, ErrorMessage = "Vượt quá độ dài cho phép")]
        [Required(ErrorMessage = "Hãy nhâp email người dùng")]
        [RegularExpression("^[a-zA-Z0-9]+(?:\\.[a-zA-Z0-9]+)*@[a-zA-Z0-9]+(?:\\.[a-zA-Z0-9]+)*$",
            ErrorMessage = "Email không hợp lệ !")]
        public string Email { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Required(ErrorMessage = "Hãy nhập ngày sinh người dùng")]
        public DateTime DateOfBirth { get; set; }

        [StringLength(20, ErrorMessage = "Vượt quá độ dài cho phép")]
        [Required(ErrorMessage = "Hãy nhập số điện thoại người dùng")]
        [RegularExpression("^(?:\\+84|0)\\d{9}$", ErrorMessage = "Số điện thoại không hợp lệ !")]
        public string PhoneNumber { get; set; }

        [StringLength(150, ErrorMessage = "Vượt quá độ dài cho phép")]
        [Required(ErrorMessage = "Hãy nhập địa chỉ nhà của người dùng")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Hãy lựa chọn giới tính của người dùng")]
        [Range(1, 2, ErrorMessage = "Giới tính người dùng không hợp lệ")]
        public Gender Gender { get; set; }

        [AllowNull]
        public BlobContentModel Avartar { get; set; }
        public string Password { get; set; }

        //navigation property
        public string classId { get; set; }
    }
}
