using AutoMapper;
using LMS_Library_API.Models;
using LMS_Library_API.Models.AboutStudent;
using LMS_Library_API.ModelsDTO;
using LMS_Library_API.Services.ClassService;
using LMS_Library_API.Services.ServiceAboutStudent.StudentSubjectService;
using LMS_Library_API.Services.ServiceAboutSubject.ClassSubjectService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LMS_Library_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ClassSubjectsController : ControllerBase
    {
        private readonly IClassSubjectSvc _classSubjectSvc;
        private readonly IStudentSubjectSvc _studentSubjectSvc;
        private readonly IClassSvc _classSvc;
        private readonly IMapper _mapper;

        public ClassSubjectsController(IClassSubjectSvc classSubjectSvc, IStudentSubjectSvc studentSubjectSvc, IClassSvc classSvc, IMapper mapper)
        {
            _classSubjectSvc = classSubjectSvc;
            _studentSubjectSvc = studentSubjectSvc;
            _classSvc = classSvc;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<Logger>> Create(ClassSubjectDTO classSubjectDTO)
        {
            var classSubject = _mapper.Map<ClassSubject>(classSubjectDTO);

            var loggerResult = await _classSubjectSvc.Create(classSubject);

            if (loggerResult.status == TaskStatus.RanToCompletion)
            {
                var listStudentInClass = await _classSvc.ListStudentInClass(classSubject.classId);

                foreach (var student in listStudentInClass)
                {
                    StudentSubject studentSubject = new StudentSubject() {
                        studentId = student.Id,
                        subjectId = classSubject.subjectId,
                        subjectMark = false
                    };

                    var addStudentSubject = await _studentSubjectSvc.Create(studentSubject);

                    if (addStudentSubject.status == TaskStatus.Faulted)
                    {
                        return BadRequest(addStudentSubject);
                    }
                }

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
            var loggerResult = await _classSubjectSvc.GetAll();
            if (loggerResult.status == TaskStatus.RanToCompletion)
            {
                return Ok(loggerResult);
            }
            else
            {
                return BadRequest(loggerResult);
            }
        }

        [HttpGet("{classId}/{subjectId}")]
        public async Task<ActionResult<Logger>> GetById(string classId, string subjectId)
        {
            if (!String.IsNullOrWhiteSpace(classId))
            {
                var loggerResult = await _classSubjectSvc.GetById(classId, subjectId);
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

        [HttpGet("class/{classId}")]
        public async Task<ActionResult<Logger>> GetByClassId(string classId)
        {
            if (!String.IsNullOrWhiteSpace(classId))
            {
                var loggerResult = await _classSubjectSvc.GetByClassId(classId);
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

        [HttpDelete]
        public async Task<ActionResult<Logger>> Delete(ClassSubjectDTO classSubjectDTO)
        {
            var classSubject = _mapper.Map<ClassSubject>(classSubjectDTO);

            if (!String.IsNullOrWhiteSpace(classSubject.classId) || !String.IsNullOrWhiteSpace(classSubject.subjectId))
            {
                var loggerResult = await _classSubjectSvc.Delete(classSubject);

                if (loggerResult.status == TaskStatus.RanToCompletion)
                {

                    var listStudentInClass = await _classSvc.ListStudentInClass(classSubject.classId);

                    foreach (var student in listStudentInClass)
                    {
                        var addStudentSubject = await _studentSubjectSvc.Delete(student.Id.ToString(), classSubject.subjectId);

                        if (addStudentSubject.status == TaskStatus.Faulted)
                        {
                            return BadRequest(addStudentSubject);
                        }
                    }

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
