using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace UploadFileToAzureBlobStorage
{
    public class BlobHelper: IBlobHelper
    {
        private readonly BlobContainerClient _blobContainerClient;

        #region ctor

        public BlobHelper(string connectionString, string containerName)
        {
            _blobContainerClient = new BlobContainerClient(connectionString, containerName);
        }

        #endregion

        #region Upload

        public async Task<BlobContentInfo> Upload(FileInfo file, bool deleteFile)
        {
            BlobContentInfo blobContentInfo = null;
            try
            {
                var blobClient = _blobContainerClient.GetBlobClient(file.Name);
                await using var fileStream = File.OpenRead(file.FullName);
                blobContentInfo = await blobClient.UploadAsync(fileStream);
                if (deleteFile) File.Delete(file.FullName);
            }
            catch (Exception ex)
            {
                throw new Exception($"Couldn't upload file. Error: {ex.Message}");
            }
            return blobContentInfo;
        }

        #endregion

        #region Download
        
        public async Task<Azure.Response> Download(string filePath, string destinationPath)
        {
            Azure.Response response = null;
            try
            {
                var blobClient = _blobContainerClient.GetBlobClient(filePath);
                response = await blobClient.DownloadToAsync(destinationPath);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            return response;
        }

        #endregion

        #region Delete

        public async Task<bool> Delete(string filePath)
        {
            try
            {
                var blobClient = _blobContainerClient.GetBlobClient(filePath);
                return await blobClient.DeleteIfExistsAsync();
            }
            catch (Exception e)
            {
                throw new Exception($"Couldn't delete file. Error: {e.Message}");
            }
        }

        #endregion

        #region Get
        public async Task<List<string>> GetAll()
        {
            List<string> result = new List<string>();
            try
            {
                await foreach (BlobItem blobItem in _blobContainerClient.GetBlobsAsync())
                {
                    result.Add(blobItem.Name);
                }
            }
            catch (Exception e)
            {
                throw new Exception($"Couldn't get blob list. Error: {e.Message}");
            }
            return result;
        }

        #endregion
    }
}
