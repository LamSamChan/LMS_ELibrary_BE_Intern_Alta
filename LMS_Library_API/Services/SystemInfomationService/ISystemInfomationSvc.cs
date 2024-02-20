using LMS_Library_API.Models;

namespace LMS_Library_API.Services.SystemInfomationService
{
    public interface ISystemInfomationSvc
    {
        Task<Logger> Create(SystemInfomation infomation);
        Task<Logger> Delete(string id);
        Task<Logger> Update(SystemInfomation infomation);
        Task<Logger> GetInfo();
    }
}
