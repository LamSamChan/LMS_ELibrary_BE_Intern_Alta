using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LMS_Library_API.ViewModels
{
    public class LoginVM
    {
        [Required(ErrorMessage = "Hãy nhâp email để đăng nhập")]
        [RegularExpression("^[a-zA-Z0-9]+(?:\\.[a-zA-Z0-9]+)*@[a-zA-Z0-9]+(?:\\.[a-zA-Z0-9]+)*$",
            ErrorMessage = "Email không hợp lệ !")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Hãy nhâp mật khẩu để đăng nhập")]
        public string Password { get; set; }
    }
}
