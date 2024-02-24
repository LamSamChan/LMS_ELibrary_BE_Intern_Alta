using AutoMapper;
using LMS_Library_API.Models;
using LMS_Library_API.Models.AboutUser;
using LMS_Library_API.Models.Exams;
using LMS_Library_API.Models.RoleAccess;
using LMS_Library_API.ModelsDTO;
using LMS_Library_API.Services.ClassService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LMS_Library_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassController : ControllerBase
    {
        private readonly IClassSvc _classSvc;
        private readonly IMapper _mapper;

        public ClassController(IClassSvc classSvc, IMapper mapper)
        {
            _classSvc = classSvc;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<Logger>> Create(ClassDTO classDTO)
        {
            var newClass = _mapper.Map<Class>(classDTO);
            newClass.totalStudent = 0;
            var loggerResult = await _classSvc.Create(newClass);
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
            var loggerResult = await _classSvc.GetAll();
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
        public async Task<ActionResult<Logger>> GetById(string id)
        {
            if (!String.IsNullOrWhiteSpace(id))
            {
                var loggerResult = await _classSvc.GetById(id.ToUpper());

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
        public async Task<ActionResult<Logger>> Update(ClassDTO classDTO)
        {

            var dataClass = _mapper.Map<Class>(classDTO);

            var loggerResult = await _classSvc.Update(dataClass);
            if (loggerResult.status == TaskStatus.RanToCompletion)
            {
                return Ok(loggerResult);
            }
            else
            {
                return BadRequest(loggerResult);
            }
        }

        [HttpGet("search/{query}")]
        public async Task<ActionResult<Logger>> Search(string query)
        {
            if (!String.IsNullOrWhiteSpace(query))
            {
                var loggerResult = await _classSvc.Search(query);

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
                return BadRequest("Hãy điền nội dung để tìm kiếm đối tượng");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Role>> Detele(string id)
        {
            if (!String.IsNullOrWhiteSpace(id))
            {
                var loggerResult = await _classSvc.Delete(id);
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
