using System;
using System.Collections.Generic;
using System.IO;
using Azure.Storage.Blobs;

namespace UploadFileToAzureBlobStorage
{
    public class BlobHelper
    {
        private readonly BlobContainerClient _blobContainerClient;
        public BlobHelper(string connectionString, string containerName)
        {
            _blobContainerClient = new BlobContainerClient(connectionString, containerName);
        }

        /// <summary>
        /// Takes list of files and uploads them all
        /// </summary>
        /// <param name="files"></param>
        /// <param name="deleteFile"></param>
        public void UploadFiles(IEnumerable<FileInfo> files, bool deleteFile)
        {
            try
            {
                foreach (var file in files)
                {
                    Upload(file, deleteFile);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        /// <summary>
        /// Takes a single file and uploads that
        /// </summary>
        /// <param name="file"></param>
        /// <param name="deleteFile"></param>
        public void UploadFile(FileInfo file, bool deleteFile)
        {
            try
            {
                Upload(file, deleteFile);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private void Upload(FileInfo file, bool deleteFile)
        {
            var blobClient = _blobContainerClient.GetBlobClient(file.Name);
            using var fileStream = File.OpenRead(file.FullName);
            blobClient.Upload(fileStream);
            if (deleteFile) File.Delete(file.FullName);
        }
    }
}
