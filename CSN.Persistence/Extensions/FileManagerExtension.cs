using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSN.Persistence.Extensions
{
    public static class FileManagerExtension
    {
        private const string path = "../CSN.Persistence/Uploads";

        public async static Task<string?> WriteToFileAsync(this byte[]? image, string fileName)
        {
            string fullName = $"{Guid.NewGuid()}.{fileName}.{DateTime.Now.ToString("ddMMyyyy.HHmm")}";
            string fullPath = $"{path}/{fullName}";

            if (image == null || image.Length <= 0) return null;

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            await File.WriteAllBytesAsync(fullPath, image);
            return fullPath;
        }

        public async static Task<byte[]?> ReadToBytesAsync(this string? path)
        {
            if (path == null) return null;
            return await File.ReadAllBytesAsync(path);
        }


        public static byte[]? ReadToBytes(this string? path)
        {
            if (path == null) return null;
            return File.ReadAllBytes(path);
        }
    }
}