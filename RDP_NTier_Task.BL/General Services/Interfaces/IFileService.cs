using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace RDP_NTier_Task.BL.General_Services
{
    public interface IFileService
    {

           Task<string> SaveFile(IFormFile file, string folderPath);
           Task<int> DeleteFile(string fileName, string folderPath);
        Task<List<string>> SaveManyFiles(List<IFormFile> files, string folderPath);



    }
}
