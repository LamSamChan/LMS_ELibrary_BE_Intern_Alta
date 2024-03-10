using AutoMapper;
using LMS_Library_API.Models;
using LMS_Library_API.Models.AboutStudent;
using LMS_Library_API.Models.Notification;
using LMS_Library_API.ModelsDTO;
using LMS_Library_API.Services.ServiceAboutStudent.StudyTimeService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LMS_Library_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class StudyTimesController : ControllerBase
    {
        private readonly IStudyTimeSvc _studyTimeSvc;
        private readonly IMapper _mapper;

        public StudyTimesController(IStudyTimeSvc studyTimeSvc, IMapper mapper)
        {
            _studyTimeSvc = studyTimeSvc;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<Logger>> Create(StudyTimeDTO studyTimeDTO)
        {
            var studyTime = _mapper.Map<StudyTime>(studyTimeDTO);

            var loggerResult = await _studyTimeSvc.Create(studyTime);
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
            var loggerResult = await _studyTimeSvc.GetAll();
            if (loggerResult.status == TaskStatus.RanToCompletion)
            {
                return Ok(loggerResult);
            }
            else
            {
                return BadRequest(loggerResult);
            }
        }

        [HttpGet("{studentId}/{subjectId}/{date}")]
        public async Task<ActionResult<Logger>> GetById(string studentId, string subjectId, DateTime date)
        {
            if (!String.IsNullOrWhiteSpace(studentId))
            {
                var loggerResult = await _studyTimeSvc.GetById(studentId, subjectId, date);
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
        public async Task<ActionResult<Logger>> GetByUserId(string studentId)
        {
            if (!String.IsNullOrWhiteSpace(studentId))
            {
                var loggerResult = await _studyTimeSvc.GetByStudentId(studentId);
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
        public async Task<ActionResult<Logger>> Update(StudyTimeDTO studyTimeDTO)
        {
            var studyTime = _mapper.Map<StudyTime>(studyTimeDTO);

            var loggerResult = await _studyTimeSvc.Update(studyTime);
            if (loggerResult.status == TaskStatus.RanToCompletion)
            {
                return Ok(loggerResult);
            }
            else
            {
                return BadRequest(loggerResult);
            }
        }

        [HttpDelete("{studentId}/{subjectId}/{date}")]
        public async Task<ActionResult<Logger>> Delete(string studentId, string subjectId, DateTime date)
        {
            if (!String.IsNullOrWhiteSpace(studentId))
            {
                var loggerResult = await _studyTimeSvc.Delete(studentId, subjectId, date);
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
