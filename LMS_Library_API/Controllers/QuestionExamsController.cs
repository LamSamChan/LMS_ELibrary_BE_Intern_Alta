using AutoMapper;
using LMS_Library_API.Models;
using LMS_Library_API.Models.Exams;
using LMS_Library_API.Models.RoleAccess;
using LMS_Library_API.ModelsDTO;
using LMS_Library_API.Services.ServiceAboutExam.Question_ExamService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LMS_Library_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionExamsController : ControllerBase
    {
        private readonly IQuestionExamSvc _questionExamSvc;
        private readonly IMapper _mapper;

        public QuestionExamsController(IQuestionExamSvc questionExamSvc, IMapper mapper)
        {
            _questionExamSvc = questionExamSvc;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<Logger>> Create(Question_ExamDTO questionDTO)
        {
            var checkFormat = await _questionExamSvc.CheckFormat(questionDTO.ExamId, questionDTO.QuestionId);
            if (checkFormat.status == TaskStatus.Faulted)
            {
                return checkFormat;
            }

            var newQuestion = _mapper.Map<Question_Exam>(questionDTO);
            var loggerResult = await _questionExamSvc.Create(newQuestion);
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
            var loggerResult = await _questionExamSvc.GetAll();
            if (loggerResult.status == TaskStatus.RanToCompletion)
            {
                return Ok(loggerResult);
            }
            else
            {
                return BadRequest(loggerResult);
            }
        }

        [HttpGet("{examId}/{questionId}")]
        public async Task<ActionResult<Logger>> GetById(string examId, int questionId)
        {
            if (!String.IsNullOrWhiteSpace(examId) && !String.IsNullOrWhiteSpace(questionId.ToString()))
            {
                var loggerResult = await _questionExamSvc.GetById(examId, questionId);

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
        public async Task<ActionResult<Logger>> Update(Question_ExamDTO questionDTO)
        {
            var checkFormat = await _questionExamSvc.CheckFormat(questionDTO.ExamId, questionDTO.QuestionId);
            if (checkFormat.status == TaskStatus.Faulted)
            {
                return checkFormat;
            }


            var dataQuestion = _mapper.Map<Question_Exam>(questionDTO);

            var loggerResult = await _questionExamSvc.Update(dataQuestion);
            if (loggerResult.status == TaskStatus.RanToCompletion)
            {
                return Ok(loggerResult);
            }
            else
            {
                return BadRequest(loggerResult);
            }
        }

        [HttpDelete("{examId}/{questionId}")]
        public async Task<ActionResult<Role>> Detele(string examId, int questionId)
        {
            if (!String.IsNullOrWhiteSpace(examId))
            {
                var loggerResult = await _questionExamSvc.Delete(examId, questionId);
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
