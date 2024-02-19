using LMS_Library_API.Helpers.BlobHelperService;
using LMS_Library_API.Models;
using LMS_Library_API.Models.AboutUser;
using LMS_Library_API.Models.BlobStorage;
using LMS_Library_API.ModelsDTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<ActionResult<Logger>> Create(BlobContentModel uploadModel)
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
        /// image => list file trong container "image"
        /// document => list file trong container "document"
        /// </summary>
        [HttpGet("GetListFile")]
        public async Task<ActionResult<Logger>> GetAll(string container)
        {
            var loggerResult = await _blobStorageSvc.ListFileBlobs(container);
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
