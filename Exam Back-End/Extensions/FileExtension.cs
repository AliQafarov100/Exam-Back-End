using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Exam_Back_End.Extensions
{
    public static class FileExtension
    {
        public static bool IsOkay(this IFormFile file,int mb)
        {
            return file.Length < mb * 1024 * 1024 && file.ContentType.Contains("image");
        }
        public static bool IsMoreMb(this IFormFile file, int mb)
        {
            return file.Length < mb * 1024 * 1024;
        }
    }
}
