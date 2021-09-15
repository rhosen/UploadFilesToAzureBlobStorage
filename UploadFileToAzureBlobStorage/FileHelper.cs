using System;
using System.IO;
using System.Linq;
using Microsoft.Extensions.Configuration;

namespace UploadFileToAzureBlobStorage
{
    public static class FileHelper
    {
        private static readonly DirectoryInfo DirectoryInfo;
        private static readonly string AppSettingPath;
        private static readonly ConfigurationBuilder ConfigurationBuilder;
        public static string ConnectionString => GetConnectionString();
        public static string ContainerName => GetContainerName();
        public static FileInfo[] Files => GetFiles();

        static FileHelper()
        {
            ConfigurationBuilder = new ConfigurationBuilder();
            AppSettingPath = Path.Combine(Directory.GetCurrentDirectory(), "appSettings.json");
            DirectoryInfo = GetDirectoryPath();
        }

        static FileInfo[] GetFiles()
        {
            return DirectoryInfo.GetFiles();
        }


        public static FileInfo GetFile(string fileName)
        {
            return DirectoryInfo.GetFiles().FirstOrDefault(x=> x.FullName == fileName);
        }

        static DirectoryInfo GetDirectoryPath()
        {
            ConfigurationBuilder.AddJsonFile(AppSettingPath, false);
            var directoryPath = ConfigurationBuilder.Build().GetSection("SourceFolder").Value;
            return new DirectoryInfo(directoryPath);
        }

        static string GetConnectionString()
        {
            ConfigurationBuilder.AddJsonFile(AppSettingPath, false);
            return ConfigurationBuilder.Build().GetSection("ConnectionString").Value;
        }

        static string GetContainerName()
        {
            ConfigurationBuilder.AddJsonFile(AppSettingPath, false);
            return ConfigurationBuilder.Build().GetSection("Container").Value;
        }

        public static void PrintFileNames(FileInfo[] files)
        {
            Console.WriteLine($"Printing all file names in: [{DirectoryInfo}]");
            Console.WriteLine("================================================= \n");
            foreach (var file in files)
            {
                Console.WriteLine(file.FullName);
            }
            Console.ReadLine();
        }

    }
}
