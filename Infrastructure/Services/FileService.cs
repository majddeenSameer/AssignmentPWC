using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces;

namespace Infrastructure.Services
{
    public class FileService : IFileService
    {
        private string _folderPath;
        public FileService(IConfiguration configuration, ILogger<FileService> logger)
        {
            _folderPath = configuration["Storage:FolderPath"];
        }

        public async Task<byte[]> GetAsync(string key)
        {
           
                var filePath = Path.Combine(_folderPath, key);
                return File.ReadAllBytes(filePath);
        }
        public async Task<string> SaveAsync(string key, byte[] content)
        {

            var filePath = Path.Combine(_folderPath, key);
            var dirPath = Path.GetDirectoryName(filePath);

            if (!Directory.Exists(dirPath))
                Directory.CreateDirectory(dirPath);

            File.WriteAllBytes(filePath, content);
            return string.Empty;
        }

        public async Task DeleteAsync(string key)
        {

            var filePath = Path.Combine(_folderPath, key);
            File.Delete(filePath);
        }
    }
}
