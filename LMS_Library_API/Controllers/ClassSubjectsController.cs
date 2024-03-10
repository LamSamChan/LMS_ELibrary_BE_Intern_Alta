using AutoMapper;
using LMS_Library_API.Models;
using LMS_Library_API.Models.AboutStudent;
using LMS_Library_API.ModelsDTO;
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
        private readonly IMapper _mapper;

        public ClassSubjectsController(IClassSubjectSvc classSubjectSvc, IMapper mapper)
        {
            _classSubjectSvc = classSubjectSvc;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<Logger>> Create(ClassSubjectDTO classSubjectDTO)
        {
            var studyHistory = _mapper.Map<ClassSubject>(classSubjectDTO);

            var loggerResult = await _classSubjectSvc.Create(studyHistory);
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

        [HttpPut]
        public async Task<ActionResult<Logger>> Update(ClassSubjectDTO classSubjectDTO)
        {
            var classSubject = _mapper.Map<ClassSubject>(classSubjectDTO);

            var loggerResult = await _classSubjectSvc.Update(classSubject);
            if (loggerResult.status == TaskStatus.RanToCompletion)
            {
                return Ok(loggerResult);
            }
            else
            {
                return BadRequest(loggerResult);
            }
        }

        [HttpDelete("{classId}/{subjectId}")]
        public async Task<ActionResult<Logger>> Delete(string classId, string subjectId)
        {
            if (!String.IsNullOrWhiteSpace(classId) || !String.IsNullOrWhiteSpace(subjectId))
            {
                var loggerResult = await _classSubjectSvc.Delete(classId, subjectId);
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
