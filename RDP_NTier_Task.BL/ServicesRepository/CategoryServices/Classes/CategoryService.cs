using Mapster;
using RDP_NTier_Task.BL.ServicesRepository.GenericRepositoryServices;
using RDP_NTier_Task.BL.ServicesRepostry.ProductServices.Interfaces;
using RDP_NTier_Task.DAL.DTO.CategoryDTO;
using RDP_NTier_Task.DAL.DTO.ResponseDTO;
using RDP_NTier_Task.DAL.Models;
using RDP_NTier_Task.DAL.Repostry.CategoryRepostry;
using RDP_NTier_Task.DAL.Repostry.GenericReporstry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDP_NTier_Task.BL.ServicesRepostry.ProductServices.Classes
{
    public class CategoryService : GenericRepositoryServices<Category,CategoryRequest,CategoryResponse>,ICategoryService
    {
       public CategoryService(IGenericRepository<Category> repository) : base(repository)
        {

        }

       
    }
}
