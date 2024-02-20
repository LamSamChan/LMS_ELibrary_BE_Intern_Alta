using LMS_Library_API.Models;
using LMS_Library_API.Models.BlobStorage;

namespace LMS_Library_API.Helpers.BlobHelperService
{
    public interface IBlobStorageSvc
    {
        Task<BlobObject> GetBlobFile(string filePath, string containerName);
        Task<Logger> UploadBlobFile (BlobContentModel uploadModel);
        Task<Logger> DeleteBlobFile (string filePath,string containerName);
        Task<Logger> ListFileBlobs(string containerName);
        Task<Logger> ChangeFileName(string fileName, string newFileName);
    }
}
