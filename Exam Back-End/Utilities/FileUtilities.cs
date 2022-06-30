using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Exam_Back_End.Utilities
{
    public static class FileUtilities
    {
        public async static Task<string> FileCreate(this IFormFile file,string root,string folder)
        {
            string fileName = file.FileName;
            string filePath = Path.Combine(root, folder);
            string fullPath = Path.Combine(filePath, fileName);

            using(FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return fileName;
        }
    }
}
