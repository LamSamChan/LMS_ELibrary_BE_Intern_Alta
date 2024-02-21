using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using LMS_Library_API.Constants;
using LMS_Library_API.Models;
using LMS_Library_API.Models.BlobStorage;
using System.ComponentModel;
using System.Net.WebSockets;

namespace LMS_Library_API.Helpers.BlobHelperService
{
    public class BlobStorageSvc : IBlobStorageSvc
    {
        private readonly BlobServiceClient _blobServiceClient;
        private BlobContainerClient _imgBlobContainerClient;
        private BlobContainerClient _docBlobContainerClient;

        public BlobStorageSvc(BlobServiceClient blobServiceClient)
        {
            _blobServiceClient = blobServiceClient;
            _imgBlobContainerClient = _blobServiceClient.GetBlobContainerClient("imagecontainer");
            _docBlobContainerClient = _blobServiceClient.GetBlobContainerClient("documentcontainer");
        }

        public async Task<Logger> ChangeFileName(string fileName, string newFileName)
        {

            if (string.Equals(fileName,newFileName))
            {
                return new Logger
                {
                    status = TaskStatus.RanToCompletion,
                    message = "Đổi tên tệp thành công",
                    data = newFileName
                };
            }


            var blobClient = _docBlobContainerClient.GetBlobClient(fileName);

            if (await blobClient.ExistsAsync())
            {
                var copyBlobClient = _docBlobContainerClient.GetBlobClient(newFileName);

                await copyBlobClient.StartCopyFromUriAsync(blobClient.Uri);
                await blobClient.DeleteIfExistsAsync();
                return new Logger
                {
                    status = TaskStatus.RanToCompletion,
                    message = "Đổi tên tệp thành công",
                    data = copyBlobClient.Uri.AbsoluteUri
                };
            }
            else
            {
                var imageBlobClient = _imgBlobContainerClient.GetBlobClient(fileName);
                if (await imageBlobClient.ExistsAsync())
                {
                    var copyBlobClient = _imgBlobContainerClient.GetBlobClient(newFileName);

                    await copyBlobClient.StartCopyFromUriAsync(imageBlobClient.Uri);
                    await imageBlobClient.DeleteIfExistsAsync();
                    return new Logger
                    {
                        status = TaskStatus.RanToCompletion,
                        message = "Đổi tên tệp thành công",
                        data = copyBlobClient.Uri.AbsoluteUri
                    };
                }
                else
                {
                    return new Logger
                    {
                        status = TaskStatus.Faulted,
                        message = $"Không tồn tại tệp {fileName}",
                    };
                }      
            }
        }

        public async Task<Logger> DeleteBlobFile(string filePath, string containerName)
        {
            var fileName = new Uri(filePath).Segments.LastOrDefault();
            BlobClient blobClient;
            if (string.Equals(containerName, "image", StringComparison.OrdinalIgnoreCase))
            {
                blobClient = _imgBlobContainerClient.GetBlobClient(fileName);
            }
            else if (string.Equals(containerName, "document", StringComparison.OrdinalIgnoreCase))
            {
                blobClient = _docBlobContainerClient.GetBlobClient(fileName);
            }
            else
            {
                return new Logger
                {
                    status = TaskStatus.Faulted,
                    message = "Không tìm thấy file cần xóa, hãy kiểm tra lại container hoặc đường dẫn",
                };
            }

            await blobClient.DeleteIfExistsAsync();

            return new Logger
            {
                status = TaskStatus.RanToCompletion,
                message = "Xóa tệp thành công",
            };

        }

        public async Task<BlobObject> GetBlobFile(string filePath, string containerName)
        {
            var fileName = new Uri(filePath).Segments.LastOrDefault();

            try
            {
                BlobClient blobClient;
                if (string.Equals(containerName, "image", StringComparison.OrdinalIgnoreCase))
                {
                   blobClient = _imgBlobContainerClient.GetBlobClient(fileName);
                }
                else if (string.Equals(containerName, "document", StringComparison.OrdinalIgnoreCase))
                {
                    blobClient = _docBlobContainerClient.GetBlobClient(fileName);
                }
                else
                {
                    return null;
                }

                if (await blobClient.ExistsAsync())
                {
                    BlobDownloadResult content = await blobClient.DownloadContentAsync();
                    var downloadData = content.Content.ToStream();

                    if (string.Equals(containerName, "image", StringComparison.OrdinalIgnoreCase))
                    {
                       var extension = Path.GetExtension(fileName);
                       return new BlobObject { Content = downloadData,ContentType="image/"+ extension.Remove(0,1) ,FileName = fileName };
                    }
                    else
                    {
                       return new BlobObject { Content = downloadData, ContentType = content.Details.ContentType, FileName = fileName };
                    }

                }
            }
            catch (Exception ex)
            {
                return null;
            }

            return null;
        }

        public async Task<Logger> ListFileBlobs(string containerName)
        {
            List<string> blobList = new List<string>();

            AsyncPageable<BlobItem> getListFromContainer;

            if (string.Equals(containerName, "image", StringComparison.OrdinalIgnoreCase))
            {
                getListFromContainer = _imgBlobContainerClient.GetBlobsAsync();
            }
            else if (string.Equals(containerName, "document", StringComparison.OrdinalIgnoreCase))
            {
                getListFromContainer = _docBlobContainerClient.GetBlobsAsync();

            }
            else
            {
                return new Logger
                {
                    status = TaskStatus.Faulted,
                    message = "Không tìm thấy container",
                };
            }

            await foreach(var item in getListFromContainer)
            {
                blobList.Add(item.Name);
            }

            return new Logger
            {
                status = TaskStatus.RanToCompletion,
                message = "Tải tệp lên thành công",
                listData = blobList
            };
        }

        public async Task<Logger> UploadBlobFile(BlobContentModel uploadModel)
        {
            string fileExtension = Path.GetExtension(uploadModel.FileName);

            string contentType = ChooseContentType(fileExtension);

            if (contentType == null)
            {
                return new Logger
                {
                    status = TaskStatus.Faulted,
                    message = "File không hợp lệ"
                };
            }

            var fileName = Guid.NewGuid().ToString();
            BlobClient blobClient;
            if (uploadModel.isImage)
            {
                blobClient = _imgBlobContainerClient.GetBlobClient(fileName + fileExtension);
            }
            else
            {
                blobClient = _docBlobContainerClient.GetBlobClient(fileName + fileExtension);
            }

            try
            {
                await blobClient.UploadAsync(uploadModel.FilePath, new BlobHttpHeaders { ContentType = contentType });

                return new Logger
                {
                    status = TaskStatus.RanToCompletion,
                    message = "Tải tệp lên thành công",
                    data = blobClient.Uri.AbsoluteUri
                };
            }
            catch (Exception ex)
            {

                return new Logger
                {
                    status = TaskStatus.Faulted,
                    message = ex.Message
                };
            }
        }

        private string ChooseContentType(string fileExtenstion)
        {
            foreach (var ex in MimeType.ListMimeType)
            {
                if (string.Equals(fileExtenstion,ex.Key,StringComparison.OrdinalIgnoreCase))
                {
                    return ex.Value;
                }
            }
            return null;
        }

        private async Task<Logger> CheckBlobExist(BlobClient blobClient)
        {
            if (await blobClient.ExistsAsync())
            {
                return new Logger
                {
                    status = TaskStatus.Faulted,
                    message = "Tên tệp đã tồn tại",
                };
            }
            else
            {
                return new Logger
                {
                    status = TaskStatus.RanToCompletion,
                };
            }
        }
    }
}
