using AutoMapper;
using LMS_Library_API.Models;
using LMS_Library_API.Models.AboutStudent;
using LMS_Library_API.ModelsDTO;
using LMS_Library_API.Services.ServiceAboutStudent.StudentSubjectService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LMS_Library_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class StudentSubjectsController : ControllerBase
    {
        private readonly IStudentSubjectSvc _studentSubjectSvc;
        private readonly IMapper _mapper;

        public StudentSubjectsController(IStudentSubjectSvc studentSubjectSvc, IMapper mapper)
        {
            _studentSubjectSvc = studentSubjectSvc;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<Logger>> Create(StudentSubjectDTO studentSubjectDTO)
        {
            var studentSubject = _mapper.Map<StudentSubject>(studentSubjectDTO);

            var loggerResult = await _studentSubjectSvc.Create(studentSubject);
            if (loggerResult.status == TaskStatus.RanToCompletion)
            {
                return Ok(loggerResult);
            }
            else
            {
                return BadRequest(loggerResult);
            }
        }

        [HttpGet]
        public async Task<ActionResult<Logger>> GetAll()
        {
            var loggerResult = await _studentSubjectSvc.GetAll();
            if (loggerResult.status == TaskStatus.RanToCompletion)
            {
                return Ok(loggerResult);
            }
            else
            {
                return BadRequest(loggerResult);
            }
        }

        [HttpGet("{studentId}/{subjectId}")]
        public async Task<ActionResult<Logger>> GetById(string studentId, string subjectId)
        {
            if (!String.IsNullOrWhiteSpace(studentId))
            {
                var loggerResult = await _studentSubjectSvc.GetById(studentId, subjectId);
                if (loggerResult.status == TaskStatus.RanToCompletion)
                {
                    return Ok(loggerResult);
                }
                else
                {
                    return BadRequest(loggerResult);
                }
            }
            else
            {
                return BadRequest("Hãy điền ID để tìm kiếm đối tượng");
            }
        }

        [HttpGet("student/{studentId}")]
        public async Task<ActionResult<Logger>> GetByStudentId(string studentId)
        {
            if (!String.IsNullOrWhiteSpace(studentId))
            {
                var loggerResult = await _studentSubjectSvc.GetByStudentId(studentId);
                if (loggerResult.status == TaskStatus.RanToCompletion)
                {
                    return Ok(loggerResult);
                }
                else
                {
                    return BadRequest(loggerResult);
                }
            }
            else
            {
                return BadRequest("Hãy điền ID để tìm kiếm đối tượng");
            }
        }

        [HttpPut]
        public async Task<ActionResult<Logger>> Update(StudentSubjectDTO studentSubjectDTO)
        {
            var studentSubject = _mapper.Map<StudentSubject>(studentSubjectDTO);

            var loggerResult = await _studentSubjectSvc.Update(studentSubject);
            if (loggerResult.status == TaskStatus.RanToCompletion)
            {
                return Ok(loggerResult);
            }
            else
            {
                return BadRequest(loggerResult);
            }
        }

        [HttpDelete("{studentId}/{subjectId}")]
        public async Task<ActionResult<Logger>> Delete(string studentId, string subjectId)
        {
            if (!String.IsNullOrWhiteSpace(studentId) || !String.IsNullOrWhiteSpace(subjectId))
            {
                var loggerResult = await _studentSubjectSvc.Delete(studentId, subjectId);
                if (loggerResult.status == TaskStatus.RanToCompletion)
                {
                    return Ok(loggerResult);
                }
                else
                {
                    return BadRequest(loggerResult);
                }
            }
            else
            {
                return BadRequest("Hãy điền ID để xoá đối tượng");
            }
        }
    }
}
