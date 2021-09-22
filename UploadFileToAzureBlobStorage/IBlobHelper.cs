using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Azure.Storage.Blobs.Models;

namespace UploadFileToAzureBlobStorage
{
    interface IBlobHelper
    {
        Task<BlobContentInfo> Upload(FileInfo file, bool deleteFile);
        Task<Azure.Response> Download(string filePath, string destinationPath);
        Task<bool> Delete(string filePath);
        Task<List<string>> GetAll();
    }
}
