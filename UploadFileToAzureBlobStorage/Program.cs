using System;
namespace UploadFileToAzureBlobStorage
{
    class Program
    {
        static void Main(string[] args)
        {
            //TODO: Don't forget to configure the appSetting.json file
            //TODO: Use this one if you want to upload multiple files at once
            //var files = FileHelper.Files;
            //Prints all the file names you have in the directory
            //FileHelper.PrintFileNames(files);
            //TODO: Check validation for multiple files
            //if (!files.Any()) Console.WriteLine("Nothing to process");
            var file = FileHelper.GetFile("C:\\BlobTest\\Late Night Poetry 2.txt");
            if (file == null) Console.WriteLine("Nothing to process");
            else
            {
                var blobHelper = new BlobHelper(FileHelper.ConnectionString, FileHelper.ContainerName);
                // TODO: Upload multiple files
                //blobHelper.UploadFiles(FileHelper.Files, FileHelper.ConnectionString, FileHelper.ContainerName);
                Console.WriteLine("Uploading...");
                blobHelper.UploadFile(file, false);
                Console.WriteLine("Uploaded.");
            }
        }
    }
}