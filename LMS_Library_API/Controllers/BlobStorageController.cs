using LMS_Library_API.Helpers.BlobHelperService;
using LMS_Library_API.Models;
using LMS_Library_API.Models.AboutUser;
using LMS_Library_API.Models.BlobStorage;
using LMS_Library_API.ModelsDTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace LMS_Library_API.Controllers
{
    

    [Route("api/[controller]")]
    [ApiController]

    public class BlobStorageController : ControllerBase
    {

        private readonly IBlobStorageSvc _blobStorageSvc;
        public BlobStorageController(IBlobStorageSvc blobStorageSvc) {
            _blobStorageSvc = blobStorageSvc;
        }

        [HttpPost("UploadFile")]
        public async Task<ActionResult<Logger>> Upload(BlobContentModel uploadModel)
        {
            var loggerResult = await _blobStorageSvc.UploadBlobFile(uploadModel);
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
        /// containerName: image => thao tác với file trong container "image" |
        /// containerName: document => thao tác với file trong container "document"
        /// </summary>
        /// 
        [HttpGet("GetBlobFile")]
        public async Task<ActionResult<Logger>> GetBlobFile(string filePath, string containerName)
        {
            BlobObject blobObject = await _blobStorageSvc.GetBlobFile(filePath, containerName);

            if (blobObject == null)
            {
                return BadRequest(new Logger
                {
                    status = TaskStatus.Faulted,
                    message = "Container hoặc File không tồn tại, hãy kiểm tra lại"
                });
            }
            if (blobObject.ContentType.Contains("image"))
            {
                return File(blobObject.Content, blobObject.ContentType);
            }
            else
            {
                return File(blobObject.Content, blobObject.ContentType, blobObject.FileName);
            }
        }

        /// <summary>
        /// containerName: image => thao tác với file trong container "image" |
        /// containerName: document => thao tác với file trong container "document"
        /// </summary>
        /// 
        [HttpGet("GetListFile/{containerName}")]
        public async Task<ActionResult<Logger>> GetAll(string containerName)
        {
            var loggerResult = await _blobStorageSvc.ListFileBlobs(containerName);
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
        /// containerName: image => thao tác với file trong container "image" |
        /// containerName: document => thao tác với file trong container "document"
        /// </summary>
        /// 
        [HttpDelete("DeleteFile")]
        public async Task<ActionResult<Logger>> DeleteBlobFile(string filePath, string containerName)
        {
            var loggerResult = await _blobStorageSvc.DeleteBlobFile(filePath,containerName);
            if (loggerResult.status == TaskStatus.RanToCompletion)
            {
                return Ok(loggerResult);
            }
            else
            {
                return BadRequest(loggerResult);
            }
        }

    }
}
