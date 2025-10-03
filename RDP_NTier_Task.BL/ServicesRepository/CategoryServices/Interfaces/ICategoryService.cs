using RDP_NTier_Task.BL.ServicesRepository.GenericRepositoryServices;
using RDP_NTier_Task.DAL.DTO.CategoryDTO;
using RDP_NTier_Task.DAL.DTO.ResponseDTO;
using RDP_NTier_Task.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDP_NTier_Task.BL.ServicesRepostry.ProductServices.Interfaces
{
    public interface ICategoryService : IGenericRepositoryServices<Category,CategoryRequest,CategoryResponse>
    {
      
       // you can make any thing for category here . 

    }
}
