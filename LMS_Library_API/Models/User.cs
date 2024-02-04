using LMS_Library_API.Enums;
using LMS_Library_API.Models.AboutSubject;
using LMS_Library_API.Models.AboutUser;
using LMS_Library_API.Models.Exams;
using LMS_Library_API.Models.Notification;
using LMS_Library_API.Models.RoleAccess;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace LMS_Library_API.Models
{
    public class User
    {
        [Key]
        public Guid Id { get; set; } = new Guid();

        [Column(TypeName = "nvarchar(50)")]
        [StringLength(50, ErrorMessage ="Vượt quá độ dài cho phép")]
        [Required(ErrorMessage ="Hãy nhập tên người dùng")]
        public string FullName { get; set; }

        [Column(TypeName = "varchar(50)")]
        [StringLength(50, ErrorMessage = "Vượt quá độ dài cho phép")]
        [Required(ErrorMessage ="Hãy nhâp email người dùng")]
        [RegularExpression("^[a-zA-Z0-9]+(?:\\.[a-zA-Z0-9]+)*@[a-zA-Z0-9]+(?:\\.[a-zA-Z0-9]+)*$",
            ErrorMessage = "Email không hợp lệ !")]
        public string Email { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Required(ErrorMessage ="Hãy nhập ngày sinh người dùng")]
        public DateTime DateOfBirth { get; set; }

        [Column(TypeName = "varchar(20)")]
        [StringLength(20, ErrorMessage = "Vượt quá độ dài cho phép")]
        [Required(ErrorMessage ="Hãy nhập số điện thoại người dùng")]
        [RegularExpression("^(?:\\+84|0)\\d{9}$", ErrorMessage = "Số điện thoại không hợp lệ !")]
        public string PhoneNumber { get; set; }

        [Column(TypeName = "nvarchar(150)")]
        [StringLength(150, ErrorMessage = "Vượt quá độ dài cho phép")]
        [Required(ErrorMessage ="Hãy nhập địa chỉ nhà của người dùng")]
        public string Address { get; set; }

        [Required(ErrorMessage ="Hãy lựa chọn giới tính của người dùng")]
        [Range(1,2,ErrorMessage ="Giới tính người dùng không hợp lệ")]
        public Gender Gender { get; set; }

        [AllowNull]
        public string Avartar { get; set; }
        public string Password { get; set; }

        //navigation property
        [ForeignKey("Role")]
        public int RoleId { get; set; }
        public virtual Role Role { get; set; }

        [ForeignKey("Department")]
        [Column(TypeName = "varchar(20)")]
        public string DepartmentId { get; set; }
        public virtual Department Department { get; set; }

        public virtual SystemInfomation SystemInfomation { get; set; }


        public virtual QnALikes QnALikes { get; set; }

        
        public virtual Subject Subject { get; set; }


        public virtual ICollection<Notification.Notification> Notifications { get; set; }


        [InverseProperty("User")]
        public virtual ICollection<NotificationSetting> NotificationSetting { get; set; }


        public virtual ICollection<PrivateFile> PrivateFiles { get; set; }


        public virtual ICollection<Help> Helps { get; set; }

        [InverseProperty("Censor")]
        public virtual ICollection<Exam> Censor { get; set; }

        [InverseProperty("TeacherCreated")]
        public virtual ICollection<Exam> TeacherCreated { get; set; }


        public virtual ICollection<QuestionBanks> QuestionBanks { get; set; }


        [InverseProperty("User")]
        public virtual ICollection<ExamRecentViews> ExamRecentViews { get; set; }

        [InverseProperty("Censor")]
        public virtual ICollection<Part> CensorPart { get; set; }

        [InverseProperty("TeacherCreated")]
        public virtual ICollection<Part> TeacherCreatedPart { get; set; }

        [InverseProperty("Censor")]
        public virtual ICollection<Lesson> CensorLesson { get; set; }

        [InverseProperty("TeacherCreated")]
        public virtual ICollection<Lesson> TeacherCreatedLesson { get; set; }

        public virtual ICollection<Document> CensorDocument { get; set; }

    }


}
