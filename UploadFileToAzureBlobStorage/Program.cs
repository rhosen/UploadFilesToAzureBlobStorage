using System;
using System.IO;
using System.Reflection;

namespace UploadFileToAzureBlobStorage
{
    class Program
    {
        private static IBlobHelper _blobHelper;

        static void Main(string[] args)
        {
            Init();
            //Upload();
            //GetAll();
            //Delete();
            Download();
        }

        private static void Init()
        {
            //TODO: Don't forget to configure the appSettings.json file
            var connectionString = FileHelper.ConnectionString;
            var containerName = FileHelper.ContainerName;
            _blobHelper = new BlobHelper(connectionString, containerName);
        }

        static void Upload()
        {
            var file = FileHelper.GetFile("C:\\BlobTest\\Late Night Poetry 2.txt");
            if (file == null)
            { Console.WriteLine("Nothing to process"); return; }
            var result = _blobHelper.Upload(file, false).Result;
        }

        static void Download()
        {
            var src = "Late Night Poetry 2.txt";
            string downloadPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? string.Empty, "Downloads");
            var dest = Path.Combine(downloadPath, "output.txt");
            var s = _blobHelper.Download(src, dest).Result;
        }

        static void Delete()
        {
            var src = "Late Night Poetry 2.txt";
            var r = _blobHelper.Delete(src).Result;
        }

        static void GetBlobList()
        {
            var blobList = _blobHelper.GetAll().Result;
            foreach (var blob in blobList)
            {
                Console.WriteLine(blob);
            }
        }
    }
}