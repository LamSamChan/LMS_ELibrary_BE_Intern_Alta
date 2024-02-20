using LMS_Library_API.Helpers.BlobHelperService;
using LMS_Library_API.Models.AboutUser;
using LMS_Library_API.Models;
using LMS_Library_API.ModelsDTO;
using LMS_Library_API.Services.SystemInfomationService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;

namespace LMS_Library_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SystemInfomationController : ControllerBase
    {
        private readonly ISystemInfomationSvc _systemInfomationService;
        private readonly IBlobStorageSvc _blobStorageSvc;
        private readonly IMapper _mapper;

        public SystemInfomationController( ISystemInfomationSvc systemInfomationSvc, IBlobStorageSvc blobStorageSvc, IMapper mapper) {
            _blobStorageSvc = blobStorageSvc;
            _systemInfomationService = systemInfomationSvc;
            _mapper = mapper;
        }

        /// <summary>
        /// Nếu khi tạo dữ liệu mới mà không có avartar thì gán cho trường FilePath và FileName trong đổi tượng SchoolLogo là null
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<Logger>> Create(SystemInfomationDTO infomationDTO)
        {
            var newInfomation = _mapper.Map<SystemInfomation>(infomationDTO);

            if (infomationDTO.SchoolLogo.FilePath != null && infomationDTO.SchoolLogo.FileName != null)
            {
                var logoPath = await _blobStorageSvc.UploadBlobFile(infomationDTO.SchoolLogo);

                if (logoPath.status == TaskStatus.RanToCompletion)
                {
                    newInfomation.SchoolLogo = logoPath.data.ToString();
                }
                else
                {
                    return BadRequest(logoPath);
                }
            }
            else
            {
                newInfomation.SchoolLogo = null;
            }

            var loggerResult = await _systemInfomationService.Create(newInfomation);

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
        public async Task<ActionResult<Logger>> GetInfo()
        {
            var loggerResult = await _systemInfomationService.GetInfo();
            if (loggerResult.status == TaskStatus.RanToCompletion)
            {
                return Ok(loggerResult);
            }
            else
            {
                return BadRequest(loggerResult);
            }
        }

        [HttpPut]
        public async Task<ActionResult<Logger>> Update(SystemInfomationDTO infomationDTO)
        {
            var newInfomation = _mapper.Map<SystemInfomation>(infomationDTO);

            if (infomationDTO.SchoolLogo.FilePath != null && infomationDTO.SchoolLogo.FileName != null)
            {
                var logoPath = await _blobStorageSvc.UploadBlobFile(infomationDTO.SchoolLogo);

                if (logoPath.status == TaskStatus.RanToCompletion)
                {
                    newInfomation.SchoolLogo = logoPath.data.ToString();

                    var oldInfoData = (SystemInfomation)_systemInfomationService.GetInfo().Result.data;
                    var deleteOldUserImage = _blobStorageSvc.DeleteBlobFile(oldInfoData.SchoolLogo, "image");

                    if (deleteOldUserImage.IsFaulted)
                    {
                        return BadRequest(deleteOldUserImage);
                    }
                }
                else
                {
                    return BadRequest(logoPath);
                }
            }
            else
            {
                newInfomation.SchoolLogo = null;
            }

            var loggerResult = await _systemInfomationService.Update(newInfomation);

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
        public async Task<ActionResult<Logger>> Delete(string id)
        {
            if (!String.IsNullOrWhiteSpace(id.ToString()))
            {
                var loggerResult = await _systemInfomationService.Delete(id);

                var logo = (SystemInfomation)loggerResult.data;

                if (loggerResult.status == TaskStatus.RanToCompletion)
                {
                    var deleleBlobResult = await _blobStorageSvc.DeleteBlobFile(logo.SchoolLogo,"image" );

                    if (deleleBlobResult.status == TaskStatus.RanToCompletion)
                    {
                        return Ok(loggerResult);
                    }
                    else
                    {
                        return Ok(deleleBlobResult);

                    }
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
