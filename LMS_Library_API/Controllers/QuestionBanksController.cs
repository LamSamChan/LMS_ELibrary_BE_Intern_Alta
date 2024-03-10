using AutoMapper;
using LMS_Library_API.Models;
using LMS_Library_API.Models.Exams;
using LMS_Library_API.Models.RoleAccess;
using LMS_Library_API.ModelsDTO;
using LMS_Library_API.Services.ServiceAboutExam.QuestionBankService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LMS_Library_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class QuestionBanksController : ControllerBase
    {
        private readonly IQuestionBankSvc _questionBankSvc;
        private readonly IMapper _mapper;

        public QuestionBanksController(IQuestionBankSvc questionBankSvc, IMapper mapper)
        {
            _questionBankSvc = questionBankSvc;
            _mapper = mapper;
        }

        /// <summary>
        /// Format: false -> tự luận | Level: 1 -> dễ, 2 -> trung bình, 3 -> khó
        /// </summary>

        [HttpPost("ESQuestion")]
        public async Task<ActionResult<Logger>> CreateESQuestion(QuestionBankES_DTO  questionESDTO)
        {
            var newQuestion = _mapper.Map<QuestionBanks>(questionESDTO);
            var loggerResult = await _questionBankSvc.Create(newQuestion);
            if (loggerResult.status == TaskStatus.RanToCompletion)
            {
                return Ok(loggerResult);
            }
            else
            {
                return BadRequest(loggerResult);
            }
        }

        /// <summary>
        /// Format: true -> trắc nghiệm | Level: 1 -> dễ, 2 -> trung bình, 3 -> khó
        /// </summary>
        
        [HttpPost("MCQuestion")]
        public async Task<ActionResult<Logger>> CreateMCQuestion(QuestionBankMC_DTO questionMCDTO)
        {
            var newQuestion = _mapper.Map<QuestionBanks>(questionMCDTO);
            var loggerResult = await _questionBankSvc.Create(newQuestion);
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
            var loggerResult = await _questionBankSvc.GetAll();
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
                var loggerResult = await _questionBankSvc.GetById(id);

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

        [HttpPut("ESQuestion")]
        public async Task<ActionResult<Logger>> UpdateESQuestion(QuestionBankES_DTO questionESDTO)
        {
            var dataQuestion = _mapper.Map<QuestionBanks>(questionESDTO);

            var loggerResult = await _questionBankSvc.Update(dataQuestion);
            if (loggerResult.status == TaskStatus.RanToCompletion)
            {
                return Ok(loggerResult);
            }
            else
            {
                return BadRequest(loggerResult);
            }
        }

        [HttpPut("MCQuestion")]
        public async Task<ActionResult<Logger>> UpdateMCQuestion(QuestionBankMC_DTO questionMCDTO)
        {
            var dataQuestion = _mapper.Map<QuestionBanks>(questionMCDTO);

            var loggerResult = await _questionBankSvc.Update(dataQuestion);
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
        public async Task<ActionResult<Role>> Detele(int id)
        {
            if (!String.IsNullOrWhiteSpace(id.ToString()))
            {
                var loggerResult = await _questionBankSvc.Delete(id);
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
