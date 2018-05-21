using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Configuration;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.WebPages;
using SigiApi.Models;

namespace SigiApi.Helpers
{
    public static  class FileHelper
    {

        public static  bool SaveToFile(string data,string fileName)
        {
          

            try
            {
                byte[] bytes = Convert.FromBase64String(data);

                // Create a new stream to write to the file 
                 
                using (var Writer = new BinaryWriter(File.OpenWrite(fileName)))
                {
                    // Writer raw data                
                    Writer.Write(bytes);
                }
                return true;
            }
            catch (Exception ex)
            {
                LoggerHelper.WriteLog(ex, "Error mientras se escribia Archivo en Disco: " );
                return false;
            }

        }

        public static string GetFullFileName(string filename)
        {
            //return Path.Combine(ConfigHelper.SpecialOrdersFileFolder, filename);
            return string.Empty;
        }

        public static FileUpload BuildUploadFileModel(string filepath)
        {
            var path = PathHelper.GetFullPath(filepath);
            return new FileUpload()
            {
                Data = FileHelper.SerializeToString(path),
                FileName = Path.GetFileName(path),
                MD5 = FileHelper.ComputeHash(path),
            };
        }

        public static string SerializeToString(string filePath)
        {
            if (File.Exists(filePath))
            {
                var data = File.ReadAllBytes(filePath);

                return SerializeToString(data);
            }
            return string.Empty;
        }

        private static string SerializeToString<TData>(TData settings)
        {
            using (var stream = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(stream, settings);
                stream.Flush();
                stream.Position = 0;
                return Convert.ToBase64String(stream.ToArray());
            }
        }

        private static bool IsValidContent(string filePath, string bytes)
        {
            var pdf = new FileInfo(filePath);

            //TODO get format for bytes.
            if (string.IsNullOrEmpty(bytes) && !File.Exists(filePath)) return false;

            var fileHash = new MD5CryptoServiceProvider().ComputeHash(pdf.OpenRead());
            var sum = BitConverter.ToString(fileHash).Replace("-", "").ToLower();

            return sum == bytes;
        }

        public static string ComputeHash(string filePath)
        {
            try
            {
                var hash = MD5.Create().ComputeHash(File.ReadAllBytes(filePath));
                return BitConverter.ToString(hash).Replace("-", "");
            }finally {}
        }
    }
}
