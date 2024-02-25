using LMS_Library_API.Models.AboutUser;
using LMS_Library_API.Models;
using LMS_Library_API.ModelsDTO;
using LMS_Library_API.Services.ServiceAboutUser.QnALikesService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using LMS_Library_API.Services.ServiceAboutStudent.StudentQnALikeService;
using LMS_Library_API.Models.AboutStudent;

namespace LMS_Library_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentQnALikesController : ControllerBase
    {
        private readonly IStudentQnALikesService _qnALikesSvc;

        public StudentQnALikesController(IStudentQnALikesService qnALikesSvc)
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

            List<StudentQnALikesDTO> qnaDTOList = new List<StudentQnALikesDTO>();
            foreach (var item in qnALikes)
            {
                StudentQnALikesDTO dto = new StudentQnALikesDTO()
                {
                    studentId = item.studentId,
                    QuestionsLikedID = JsonConvert.DeserializeObject<List<QnALikeID>>(item.QuestionsLikedID),
                    AnswersLikedID = JsonConvert.DeserializeObject<List<QnALikeID>>(item.AnswersLikedID),
                };
                qnaDTOList.Add(dto);
            }
            loggerResult.status = TaskStatus.RanToCompletion;
            loggerResult.message = "Thàng công";
            loggerResult.listData = new List<object>() { qnaDTOList };

            return Ok(loggerResult);
        }

        [HttpPut]
        public async Task<ActionResult<Logger>> Update(StudentQnALikesDTO qnALikesDTO)
        {
            StudentQnALikes qnALikes = new StudentQnALikes()
            {
                studentId = qnALikesDTO.studentId,
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

        [HttpGet("student/{id}")]
        public async Task<ActionResult<Logger>> GetByStudentId(string id)
        {
            if (!String.IsNullOrWhiteSpace(id))
            {
                var loggerResult = await _qnALikesSvc.GetByStudentId(id);
                if (loggerResult.status == TaskStatus.RanToCompletion)
                {
                    StudentQnALikes qnALikes = (StudentQnALikes)loggerResult.data;

                    StudentQnALikesDTO qnALikesDTO = new StudentQnALikesDTO()
                    {
                        studentId = qnALikes.studentId,
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
