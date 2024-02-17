using LMS_Library_API.Models;
using LMS_Library_API.Models.AboutUser;
using LMS_Library_API.Models.RoleAccess;
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
        public async Task<ActionResult<Logger>> GetAll()
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

        [HttpPut]
        public async Task<ActionResult<Logger>> Update(QnALikesDTO qnALikesDTO)
        {
            QnALikes qnALikes = new QnALikes()
            {
                UserId = qnALikesDTO.UserId,
                QuestionsLikedID = JsonConvert.SerializeObject(qnALikesDTO.QuestionsLikedID),
                AnswersLikedID = JsonConvert.SerializeObject(qnALikesDTO.AnswersLikedID),
            };

            var loggerResult = await _qnALikesSvc.Update(qnALikes);
            if (loggerResult.status == TaskStatus.RanToCompletion)
            {
                return Ok(loggerResult);
            }
            else
            {
                return BadRequest(loggerResult);
            }
        }

        [HttpGet("user/{id}")]
        public async Task<ActionResult<Logger>> GetByUserId(string id)
        {
            if (!String.IsNullOrWhiteSpace(id))
            {
                var loggerResult = await _qnALikesSvc.GetByUserId(id);
                if (loggerResult.status == TaskStatus.RanToCompletion)
                {
                    QnALikes qnALikes = (QnALikes)loggerResult.data;

                    QnALikesDTO qnALikesDTO = new QnALikesDTO() { 
                        UserId = qnALikes.UserId,
                        QuestionsLikedID = JsonConvert.DeserializeObject<List<QnALikeID>>(qnALikes.QuestionsLikedID),
                        AnswersLikedID = JsonConvert.DeserializeObject<List<QnALikeID>>(qnALikes.AnswersLikedID),
                    };

                    loggerResult.data = qnALikesDTO;

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

    }
}
