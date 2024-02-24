using AutoMapper;
using LMS_Library_API.Services.ClassService;
using LMS_Library_API.Services.StudentService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LMS_Library_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentSvc _studentSvc;
        private readonly IMapper _mapper;

        public StudentsController(IStudentSvc studentSvc, IMapper mapper)
        {
            _studentSvc = studentSvc;
            _mapper = mapper;
        }
    }
}
