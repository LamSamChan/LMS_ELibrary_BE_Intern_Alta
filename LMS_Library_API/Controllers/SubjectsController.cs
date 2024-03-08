using AutoMapper;
using LMS_Library_API.Models;
using LMS_Library_API.Models.Notification;
using LMS_Library_API.ModelsDTO;
using LMS_Library_API.Services.ClassService;
using LMS_Library_API.Services.SubjectService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LMS_Library_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectsController : ControllerBase
    {
        private readonly ISubjectSvc _subjectSvc;
        private readonly IClassSvc _classSvc;
        private readonly IMapper _mapper;

        public SubjectsController(ISubjectSvc subjectSvc,IClassSvc classSvc ,IMapper mapper)
        {
            _mapper = mapper;
            _subjectSvc = subjectSvc;
            _classSvc = classSvc;
        }

        [HttpPost]
        public async Task<ActionResult<Logger>> Create(SubjectDTO subjectDTO)
        {
            var subject = _mapper.Map<Subject>(subjectDTO);

            var loggerResult = await _subjectSvc.Create(subject);
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
            var loggerResult = await _subjectSvc.GetAll();
            if (loggerResult.status == TaskStatus.RanToCompletion)
            {
                return Ok(loggerResult);
            }
            else
            {
                return BadRequest(loggerResult);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Logger>> GetDetail(string id)
        {
            if (!String.IsNullOrWhiteSpace(id))
            {
                var loggerResult = await _subjectSvc.GetDetail(id);
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

        [HttpGet("getAllDoc/{id}")]
        public async Task<ActionResult<Logger>> GetAllDocument(string id)
        {
            if (!String.IsNullOrWhiteSpace(id))
            {
                var loggerResult = await _subjectSvc.GetAllDocument(id);
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

        [HttpGet("search/{query}")]
        public async Task<ActionResult<Logger>> Search(string query)
        {
            if (!String.IsNullOrWhiteSpace(query))
            {
                var loggerResult = await _subjectSvc.Search(query.Trim());
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
                return BadRequest("Hãy điền nội dung tìm kiếm");
            }
        }

        [HttpPut]
        public async Task<ActionResult<Logger>> Update(SubjectDTO subjectDTO)
        {
            var subject = _mapper.Map<Subject>(subjectDTO);

            var loggerResult = await _subjectSvc.Update(subject);
            if (loggerResult.status == TaskStatus.RanToCompletion)
            {
                return Ok(loggerResult);
            }
            else
            {
                return BadRequest(loggerResult);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Logger>> Delete(string id)
        {
            if (!String.IsNullOrWhiteSpace(id))
            {
                var loggerResult = await _subjectSvc.Delete( id);
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
