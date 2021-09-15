﻿using System;
namespace UploadFileToAzureBlobStorage
{
    class Program
    {
        static void Main(string[] args)
        {
            // TODO: Don't forget to config appSetting.json file
            // TODO: Use this one if you want to upload multiple files at once
            //var files = FileHelper.Files;
            // TODO: Prints all the file names you have in the directory
            //FileHelper.PrintFileNames(files);
            // TODO: Validation for multiple files
            //if (!files.Any()) Console.WriteLine("Nothing to process");
            var file = FileHelper.GetFile("C:\\BlobTest\\Late Night Poetry 2.txt");
            if (file == null) Console.WriteLine("Nothing to process");
            else
            {
                var blobHelper = new BlobHelper(FileHelper.ConnectionString, FileHelper.ContainerName);
                // TODO: for multiple files
                //blobHelper.UploadFiles(FileHelper.Files, FileHelper.ConnectionString, FileHelper.ContainerName);
                Console.WriteLine("Uploading...");
                blobHelper.UploadFile(file, false);
                Console.WriteLine("Uploaded.");
            }
        }
    }
}