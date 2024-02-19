﻿using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
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

        public void DeleteBlobFile(string filePath)
        {
            throw new NotImplementedException();
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

                }
            }
            catch (Exception ex)
            {
                null;
            }
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
            BlobClient blobClient;
            if (uploadModel.isImage)
            {
                blobClient = _imgBlobContainerClient.GetBlobClient(uploadModel.FileName);
            }
            else
            {
                blobClient = _docBlobContainerClient.GetBlobClient(uploadModel.FileName);
            }

            try
            {
                await blobClient.UploadAsync(uploadModel.FilePath);

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
    }
}
