using LMS_Library_API.Context;

namespace LMS_Library_API.Services.StudentService
{
    public class StudentSvc:IStudentSvc
    {
        private readonly DataContext _context;

        public StudentSvc(DataContext context)
        {
            _context = context;
        }
    }
}
