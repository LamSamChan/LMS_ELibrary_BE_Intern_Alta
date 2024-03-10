using AutoMapper;
using LMS_Library_API.Models;
using LMS_Library_API.Models.AboutStudent;
using LMS_Library_API.Models.AboutUser;
using LMS_Library_API.ModelsDTO;
using LMS_Library_API.Services.ServiceAboutStudent.StudentSubjectService;
using LMS_Library_API.Services.ServiceAboutUser.TeacherClassService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LMS_Library_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TeacherClassesController : ControllerBase
    {
        private readonly ITeacherClassSvc _teacherClassSvc;
        private readonly IMapper _mapper;

        public TeacherClassesController(ITeacherClassSvc teacherClassSvc, IMapper mapper)
        {
            _teacherClassSvc = teacherClassSvc;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<Logger>> Create(TeacherClassDTO teacherClassDTO)
        {
            var teacherClass = _mapper.Map<TeacherClass>(teacherClassDTO);

            var loggerResult = await _teacherClassSvc.Create(teacherClass);
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
            var loggerResult = await _teacherClassSvc.GetAll();
            if (loggerResult.status == TaskStatus.RanToCompletion)
            {
                return Ok(loggerResult);
            }
            else
            {
                return BadRequest(loggerResult);
            }
        }

        [HttpGet("{teacherId}/{classId}")]
        public async Task<ActionResult<Logger>> GetById(string teacherId, string classId)
        {
            if (!String.IsNullOrWhiteSpace(teacherId) || !String.IsNullOrWhiteSpace(classId))
            {
                var loggerResult = await _teacherClassSvc.GetById(teacherId, classId);
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

        [HttpGet("teacher/{teacherId}")]
        public async Task<ActionResult<Logger>> GetByStudentId(string teacherId)
        {
            if (!String.IsNullOrWhiteSpace(teacherId))
            {
                var loggerResult = await _teacherClassSvc.GetByTeacherId(teacherId);
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
        public async Task<ActionResult<Logger>> Update(TeacherClassDTO teacherClassDTO)
        {
            var teacherClass = _mapper.Map<TeacherClass>(teacherClassDTO);

            var loggerResult = await _teacherClassSvc.Update(teacherClass);
            if (loggerResult.status == TaskStatus.RanToCompletion)
            {
                return Ok(loggerResult);
            }
            else
            {
                return BadRequest(loggerResult);
            }
        }

        [HttpDelete("{teacherId}/{classId}")]
        public async Task<ActionResult<Logger>> Delete(string teacherId, string classId)
        {
            if (!String.IsNullOrWhiteSpace(teacherId) || !String.IsNullOrWhiteSpace(classId))
            {
                var loggerResult = await _teacherClassSvc.Delete(teacherId, classId);
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
