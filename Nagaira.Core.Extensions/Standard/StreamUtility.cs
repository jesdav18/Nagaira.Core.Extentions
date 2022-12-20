using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Nagaira.Core.Extentions.Standard
{
    public static class StreamUtility
    {
        public static Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>
            {
                {".txt", "text/plain"},
                {".pdf", "application/pdf"},
                {".doc", "application/vnd.ms-word"},
                {".docx", "application/vnd.ms-word"},
                {".xls", "application/vnd.ms-excel"},
                {".xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"},
                {".png", "image/png"},
                {".jpg", "image/jpeg"},
                {".jpeg", "image/jpeg"},
                {".gif", "image/gif"},
                {".csv", "text/csv"},
                {".cdr" ,"application/coreldraw"},
                {".cdr" ,"application/x-cdr"},
                {".cdr" ,"application/x-coreldraw"},
                {".cdr" ,"image/cdr"},
                {".cdr" ,"image/x-cdr"},
                {".cdr" ,"zz-application/zz-winassoc-cdr"},
                {".psd" ,"image/vnd.adobe.photoshop"},
                {".psd" ,"application/x-photoshop"},
                {".psd" ,"application/photoshop"},
                {".psd" ,"application/psd"},
                {".psd" ,"image/psd"}
            };
        }

        public static string GetMimeExtension(this string value)
        {
            return GetMimeTypes().FirstOrDefault(x => x.Value.Equals(value)).Key;
        }

        public static string GetExtensionMime(this string value)
        {
            return GetMimeTypes().FirstOrDefault(x => x.Key.Equals(value)).Value;
        }

        public static string GetFileExtension(this string fileName)
        {
            return Path.GetExtension(fileName);
        }

        public static bool IsLogitudFtpCharactersAllowed(this string value)
        {
            return value.Length < 256;
        }

        public static bool AllowedFileSize(this long bytes)
        {
            return bytes < 30000000;
        }

        public static string GetValidFileName(this string value)
        {
            value = value.Replace(" ", "");
            value = Regex.Replace(value, @"[^a-zA-z0-9 ]+", "");

            return value.GetValueDirectory();
        }

        public static string GetValueDirectory(this string value)
        {
            string invalidCharacters = Regex.Escape(new string(Path.GetInvalidFileNameChars()));
            string regStrInvalidos = string.Format(@"([{0}]*\.+$)|([{0}]+) ", invalidCharacters);

            value = Regex.Replace(value, regStrInvalidos, "_");

            return value.Replace(" ", "_");
        }
    }
}
