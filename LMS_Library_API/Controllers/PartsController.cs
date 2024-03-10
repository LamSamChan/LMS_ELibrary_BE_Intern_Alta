using AutoMapper;
using LMS_Library_API.Models;
using LMS_Library_API.Models.AboutSubject;
using LMS_Library_API.ModelsDTO;
using LMS_Library_API.Services.ServiceAboutSubject.PartService;
using LMS_Library_API.Services.SubjectService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace LMS_Library_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [SwaggerTag("Status: 0 -> Chưa gửi phê duyệt | 1: -> Đang chờ phê duyệt | 2: Đã phê duyệt | 3: Đã từ chối duyệt | 4: Đã huỷ phê duyệt")]
    public class PartsController : ControllerBase
    {
        private readonly IPartSvc _partSvc;
        private readonly IMapper _mapper;

        public PartsController(IPartSvc partSvc, IMapper mapper)
        {
            _mapper = mapper;
            _partSvc = partSvc;
        }
        /// <summary>
        /// Mặc định censorId (người kiểm duyệt) khi vừa tạo sẽ là ID của người tạo, khi có người duyệt sẽ cập nhật Id của người duyệt vào
        /// Status: 0 -> Chưa gửi phê duyệt | 1: -> Đang chờ phê duyệt | 2: Đã phê duyệt | 3: Đã từ chối duyệt | 4: Đã huỷ phê duyệt"
        /// </summary>

        [HttpPost]
        public async Task<ActionResult<Logger>> Create(PartDTO partDTO)
        {
            var part = _mapper.Map<Part>(partDTO);

            var loggerResult = await _partSvc.Create(part);
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
            var loggerResult = await _partSvc.GetAll();
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
                var loggerResult = await _partSvc.GetById(id);
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
        public async Task<ActionResult<Logger>> Update(PartDTO partDTO)
        {
            var part = _mapper.Map<Part>(partDTO);

            var loggerResult = await _partSvc.Update(part);
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
                var loggerResult = await _partSvc.Delete(id);
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
