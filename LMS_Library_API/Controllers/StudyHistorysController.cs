using AutoMapper;
using LMS_Library_API.Models;
using LMS_Library_API.Models.AboutStudent;
using LMS_Library_API.Models.Notification;
using LMS_Library_API.ModelsDTO;
using LMS_Library_API.Services.ServiceAboutStudent.StudyHistoryService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LMS_Library_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudyHistorysController : ControllerBase
    {
        private readonly IStudyHistorySvc _studyHistorySvc;
        private readonly IMapper _mapper;

        public StudyHistorysController(IStudyHistorySvc studyHistorySvc, IMapper mapper)
        {
            _studyHistorySvc = studyHistorySvc;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<Logger>> Create(StudyHistoryDTO studyHistoryDTO)
        {
            var history = _mapper.Map<StudyHistory>(studyHistoryDTO);

            var loggerResult = await _studyHistorySvc.Create(history);
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
            var loggerResult = await _studyHistorySvc.GetAll();
            if (loggerResult.status == TaskStatus.RanToCompletion)
            {
                return Ok(loggerResult);
            }
            else
            {
                return BadRequest(loggerResult);
            }
        }

        [HttpGet("{studentId}/{documentId}")]
        public async Task<ActionResult<Logger>> GetById(string studentId, int documentId)
        {
            if (!String.IsNullOrWhiteSpace(studentId))
            {
                var loggerResult = await _studyHistorySvc.GetById(studentId, documentId);
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
                var loggerResult = await _studyHistorySvc.GetByStudentId(studentId);
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
        public async Task<ActionResult<Logger>> Update(StudyHistoryDTO studyHistoryDTO)
        {
            var history = _mapper.Map<StudyHistory>(studyHistoryDTO);

            var loggerResult = await _studyHistorySvc.Update(history);
            if (loggerResult.status == TaskStatus.RanToCompletion)
            {
                return Ok(loggerResult);
            }
            else
            {
                return BadRequest(loggerResult);
            }
        }

        [HttpDelete("{studentId}/{documentId}")]
        public async Task<ActionResult<Logger>> Delete(string studentId, int documentId)
        {
            if (!String.IsNullOrWhiteSpace(studentId))
            {
                var loggerResult = await _studyHistorySvc.Delete(studentId, documentId);
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
