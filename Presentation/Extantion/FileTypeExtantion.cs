using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.Extantion
{
    public static class FileTypeExtantion
    {
        public static bool CheckFileType(this IFormFile file, string type)
        {
            return file.ContentType.Contains(type);
        }
        public static bool CheckFileSize(this IFormFile file, int kb)
        {
            return file.Length / 1024 > kb;
        }
        public static async Task<string> FileUploadAsync(this IFormFile file, params string[] folders)
        {
            string newFileName = Guid.NewGuid().ToString() + file.FileName;
            string pathFolder = Path.Combine(folders);
            string path = Path.Combine(pathFolder, newFileName);
            using (FileStream stream = new(path, FileMode.CreateNew))
            {
                await file.CopyToAsync(stream);
            }
            return newFileName;
        }

    }
}
