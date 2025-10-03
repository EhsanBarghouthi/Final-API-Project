using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDP_NTier_Task.BL.General_Services
{
    public class FileService : IFileService
    {
       
        public async Task<string> SaveFile(IFormFile file, string folderPath)
        {
            if (file == null || file.Length == 0)
                return null;

            // Ensure the folder exists
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            // Generate unique file name
            string extension = Path.GetExtension(file.FileName);
            string uniqueFileName = Guid.NewGuid().ToString() + extension;

            // Combine folder path and file name
            string fullPath = Path.Combine(folderPath, uniqueFileName);

            // Save the file
            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            return uniqueFileName; // return the saved file name
        }

        public async Task<int> DeleteFile(string file, string folderPath)
        {
            if (file == null) return 0;

            // Get full path
            string filePath = Path.Combine(folderPath, file);

            // Check if file exists and delete
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
                return 1; // Success
            }

            return 0; // File not found
        }


        public async Task<List<string>> SaveManyFiles(List<IFormFile> files, string folderPath)
        {
          var fileNames = new List<string>();

        foreach(var file in files)
            {
                if (file != null && file.Length > 0)
                {
                    // Generate unique file name
                    string extension = Path.GetExtension(file.FileName);
                    string uniqueFileName = Guid.NewGuid().ToString() + extension;

                    // Combine folder path and file name
                    string fullPath = Path.Combine(folderPath, uniqueFileName);

                    // Save the file
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }

                    fileNames.Add(uniqueFileName);
                }
            }
        return fileNames;
        }
    }
}
