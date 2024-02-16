using LMS_Library_API.Models;
using LMS_Library_API.Models.AboutUser;
using LMS_Library_API.ModelsDTO;
using LMS_Library_API.Services.ServiceAboutUser.QnALikesService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace LMS_Library_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QnALikesController : ControllerBase
    {
        private readonly IQnALikesSvc _qnALikesSvc;

        public QnALikesController (IQnALikesSvc qnALikesSvc)
        {
            _qnALikesSvc = qnALikesSvc;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<QnALikesDTO>>> GetAll()
        {
            Logger loggerResult = new Logger();
            var qnALikes = await _qnALikesSvc.GetAll();

            if (!qnALikes.Any())
            {
                loggerResult.status = TaskStatus.Faulted;
                loggerResult.message = "Có lỗi xảy ra";
                return BadRequest(loggerResult);
            }

            List<QnALikesDTO> qnaDTOList = new List<QnALikesDTO>();
            foreach (var item in qnALikes)
            {
                QnALikesDTO dto = new QnALikesDTO()
                {
                    UserId = item.UserId,
                    QuestionsLikedID = JsonConvert.DeserializeObject<List<QnALikeID>>(item.QuestionsLikedID),
                    AnswersLikedID = JsonConvert.DeserializeObject<List<QnALikeID>>(item.AnswersLikedID),
                };
                qnaDTOList.Add(dto);
            }
            loggerResult.status = TaskStatus.RanToCompletion;
            loggerResult.message = "Thàng công";
            loggerResult.listData = new List<object>(){ qnaDTOList };

            return Ok(loggerResult);
        }
        
    }
}
