using AutoMapper;
using LMS_Library_API.Models;
using LMS_Library_API.Models.AboutSubject;
using LMS_Library_API.ModelsDTO;
using LMS_Library_API.Services.ServiceAboutSubject.LessonService;
using LMS_Library_API.Services.ServiceAboutSubject.PartService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace LMS_Library_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [SwaggerTag("Status: 0 -> Chưa gửi phê duyệt | 1: -> Đang chờ phê duyệt | 2: Đã phê duyệt | 3: Đã từ chối duyệt | 4: Đã huỷ phê duyệt")]

    public class LessonsController : ControllerBase
    {

        private readonly ILessonSvc _lessonSvc;
        private readonly IMapper _mapper;

        public LessonsController(ILessonSvc lessonSvc, IMapper mapper)
        { 
            _mapper = mapper;
            _lessonSvc = lessonSvc;
        }

        /// <summary>
        /// Mặc định censorId (người kiểm duyệt) khi vừa tạo sẽ là ID của người tạo, khi có người duyệt sẽ cập nhật Id của người duyệt vào         
        /// Status: 0 -> Chưa gửi phê duyệt | 1: -> Đang chờ phê duyệt | 2: Đã phê duyệt | 3: Đã từ chối duyệt | 4: Đã huỷ phê duyệt"
        /// </summary>

        [HttpPost]
        public async Task<ActionResult<Logger>> Create(LessonDTO lessonDTO)
        {
            var lesson = _mapper.Map<Lesson>(lessonDTO);

            var loggerResult = await _lessonSvc.Create(lesson);
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
            var loggerResult = await _lessonSvc.GetAll();
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
                var loggerResult = await _lessonSvc.GetById(id);
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
        public async Task<ActionResult<Logger>> Update(LessonDTO lessonDTO)
        {
            var lesson = _mapper.Map<Lesson>(lessonDTO);

            var loggerResult = await _lessonSvc.Update(lesson);
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
                var loggerResult = await _lessonSvc.Delete(id);
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
