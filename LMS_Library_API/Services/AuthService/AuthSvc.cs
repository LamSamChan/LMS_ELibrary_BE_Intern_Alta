using Azure;
using LMS_Library_API.Context;
using LMS_Library_API.Helpers;
using LMS_Library_API.Models;
using LMS_Library_API.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace LMS_Library_API.Services.AuthService
{
    public class AuthSvc: IAuthSvc
    {
        private readonly DataContext _context;
        private readonly IEncodeHelper _encodeHelper;

        public AuthSvc(DataContext context, IEncodeHelper encodeHelper)
        {
            _context = context;
            _encodeHelper = encodeHelper;
        }

        public async Task<Logger> IsUserLogin(LoginVM loginVM)
        {
            try
            {

                var respone = _context.Users.Include(_ => _.Role).FirstOrDefault(e => e.Email.Equals(loginVM.Email) && e.Password.Equals(_encodeHelper.Encode(loginVM.Password)));

                if (respone == null)
                {
                    return new Logger()
                    {
                        status = TaskStatus.Faulted,
                        message = "Đăng nhập thất bại, hãy kiểm tra lại mật khẩu hoặc tài khoản!"
                    };
                }

                return new Logger()
                {
                    status = TaskStatus.RanToCompletion,
                    message = "Thành công",
                    data = respone
                };
            }
            catch (Exception ex)
            {
                return new Logger()
                {
                    status = TaskStatus.Faulted,
                    message = ex.Message,
                };
            }
        }

        public async Task<Logger> IsStudentLogin(LoginVM loginVM)
        {
            try
            {

                var respone = _context.Students.FirstOrDefault(e => e.Email.Equals(loginVM.Email) && e.Password.Equals(_encodeHelper.Encode(loginVM.Password)));

                if (respone == null)
                {
                    return new Logger()
                    {
                        status = TaskStatus.Faulted,
                        message = "Đăng nhập thất bại, hãy kiểm tra lại mật khẩu hoặc tài khoản!"
                    };
                }

                return new Logger()
                {
                    status = TaskStatus.RanToCompletion,
                    message = "Thành công",
                    data = respone
                };
            }
            catch (Exception ex)
            {
                return new Logger()
                {
                    status = TaskStatus.Faulted,
                    message = ex.Message,
                };
            }
        }
    }
}
