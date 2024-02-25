using AutoMapper;
using LMS_Library_API.Helpers.BlobHelperService;
using LMS_Library_API.Models.AboutStudent;
using LMS_Library_API.Models;
using LMS_Library_API.ModelsDTO;
using LMS_Library_API.Services.ClassService;
using LMS_Library_API.Services.ServiceAboutStudent.StudentQnALikeService;
using LMS_Library_API.Services.StudentService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.WebSockets;

namespace LMS_Library_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentSvc _studentSvc;
        private readonly IStudentQnALikesService _qnALikesSvc;
        private readonly IBlobStorageSvc _blobStorageSvc;
        private readonly IClassSvc _classSvc;
        private readonly IMapper _mapper;

        public StudentsController(IStudentSvc studentSvc, IStudentQnALikesService qnALikesSvc, IBlobStorageSvc blobStorageSvc, IClassSvc classSvc, IMapper mapper)
        {
            _studentSvc = studentSvc;
            _qnALikesSvc = qnALikesSvc;
            _blobStorageSvc = blobStorageSvc;
            _classSvc = classSvc;
            _mapper = mapper;
        }

        /// <summary>
        /// Nếu khi tạo người dùng mới mà không có avartar thì gán cho trường FilePath và FileName trong đổi tượng Avartar là null
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<Logger>> Create(StudentDTO student)
        {
            var newStudent = _mapper.Map<Student>(student);

            if (student.Avartar.FilePath != null && student.Avartar.FileName != null)
            {

                var avartarPath = await _blobStorageSvc.UploadBlobFile(student.Avartar);

                if (avartarPath.status == TaskStatus.RanToCompletion)
                {
                    newStudent.Avartar = avartarPath.data.ToString();
                }
                else
                {
                    return BadRequest(avartarPath);
                }
            }
            else
            {
                newStudent.Avartar = null;
            }

            var loggerResult = await _studentSvc.Create(newStudent);

            if (loggerResult.status == TaskStatus.RanToCompletion)
            {

                //Update total student in class
                var getClass = await _classSvc.GetById(newStudent.classId);

                var existClass = (Class)getClass.data;

                existClass.totalStudent = existClass.Students.Count;

                var updateTotalStudent = await _classSvc.Update(existClass);

                if (updateTotalStudent.status == TaskStatus.Faulted)
                {
                    return Ok(updateTotalStudent);
                }

                //Create QnALikeList
                Student createQnALike = (Student)loggerResult.data;

                StudentQnALikes qnALikes = new StudentQnALikes() { studentId = createQnALike.Id, QuestionsLikedID = "[]", AnswersLikedID = "[]" };
                var qnaResult = await _qnALikesSvc.Create(qnALikes);

                if (qnaResult.status == TaskStatus.RanToCompletion)
                {
                    return Ok(loggerResult);
                }
                else
                {
                    return BadRequest(qnaResult);

                }
            }
            else
            {
                return BadRequest(loggerResult);
            }
        }

        [HttpGet]
        public async Task<ActionResult<Logger>> GetAll()
        {
            var loggerResult = await _studentSvc.GetAll();
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
                var loggerResult = await _studentSvc.GetById(id);
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
        public async Task<ActionResult<Logger>> Update(StudentDTO student)
        {
            var newDataStudent = _mapper.Map<Student>(student);

            var oldStudentData = (Student)_studentSvc.GetById(student.Id.ToString()).Result.data;

            if (student.Avartar.FilePath != null && student.Avartar.FileName != null)
            {
                var avartarPath = await _blobStorageSvc.UploadBlobFile(student.Avartar);

                if (avartarPath.status == TaskStatus.RanToCompletion)
                {
                    newDataStudent.Avartar = avartarPath.data.ToString();

                    var deleteOldStudentImage = _blobStorageSvc.DeleteBlobFile(oldStudentData.Avartar, "image");

                    if (deleteOldStudentImage.IsFaulted)
                    {
                        return BadRequest(deleteOldStudentImage);
                    }
                }
                else
                {
                    return BadRequest(avartarPath);
                }
            }
            else
            {
                newDataStudent.Avartar = null;
            }

            var loggerResult = await _studentSvc.Update(newDataStudent);

            //Update total student in class
            if (newDataStudent.classId != oldStudentData.classId)
            {
                var getClass = await _classSvc.GetById(newDataStudent.classId);

                var existClass = (Class)getClass.data;

                existClass.totalStudent = existClass.Students.Count;

                var updateTotalStudent = await _classSvc.Update(existClass);

                if (updateTotalStudent.status == TaskStatus.Faulted)
                {
                    return Ok(updateTotalStudent);
                }
            }

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
                var loggerResult = await _studentSvc.Search(query.ToUpper());
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
                return BadRequest("Hãy điền nội dung tìm kiếm");
            }
        }
    }
}
