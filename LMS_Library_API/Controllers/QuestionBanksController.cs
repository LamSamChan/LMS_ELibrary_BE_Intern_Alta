using AutoMapper;
using LMS_Library_API.Services.ServiceAboutExam.QuestionBankService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LMS_Library_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionBanksController : ControllerBase
    {
        private readonly IQuestionBankSvc _questionBankSvc;
        private readonly IMapper _mapper;

        public QuestionBanksController(IQuestionBankSvc questionBankSvc, IMapper mapper)
        {
            _questionBankSvc = questionBankSvc;
            _mapper = mapper;
        }
    }
}
