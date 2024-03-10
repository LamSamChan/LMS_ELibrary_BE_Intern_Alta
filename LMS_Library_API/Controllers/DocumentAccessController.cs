using AutoMapper;
using LMS_Library_API.Models;
using LMS_Library_API.Models.AboutSubject;
using LMS_Library_API.ModelsDTO;
using LMS_Library_API.Services.ServiceAboutSubject.DocumentAccessService;
using LMS_Library_API.Services.ServiceAboutSubject.LessonAccessService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LMS_Library_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DocumentAccessController : ControllerBase
    {
        private readonly IDocumentAccessSvc _documentAccessSvc;
        private readonly IMapper _mapper;

        public DocumentAccessController(IDocumentAccessSvc documentAccessSvc, IMapper mapper)
        {
            _documentAccessSvc = documentAccessSvc;
            _mapper = mapper;
        }
        [HttpPost]
        public async Task<ActionResult<Logger>> Create(DocumentAccessDTO documentAccessDTO)
        {
            var document = _mapper.Map<DocumentAccess>(documentAccessDTO);

            var loggerResult = await _documentAccessSvc.Create(document);
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
            var loggerResult = await _documentAccessSvc.GetAll();
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
        public async Task<ActionResult<Logger>> GetById(int id)
        {
            if (!String.IsNullOrWhiteSpace(id.ToString()))
            {
                var loggerResult = await _documentAccessSvc.GetById(id);
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

        [HttpGet("class/{id}")]
        public async Task<ActionResult<Logger>> GetByClassId(string classId)
        {
            if (!String.IsNullOrWhiteSpace(classId))
            {
                var loggerResult = await _documentAccessSvc.GetByClassId(classId);
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
        public async Task<ActionResult<Logger>> Update(DocumentAccessDTO documentAccessDTO)
        {
            var documentAccess = _mapper.Map<DocumentAccess>(documentAccessDTO);

            var loggerResult = await _documentAccessSvc.Update(documentAccess);
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
        public async Task<ActionResult<Logger>> Delete(int id)
        {
            if (!String.IsNullOrWhiteSpace(id.ToString()))
            {
                var loggerResult = await _documentAccessSvc.Delete(id);
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
