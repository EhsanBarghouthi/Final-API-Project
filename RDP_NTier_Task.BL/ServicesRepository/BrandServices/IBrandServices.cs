using RDP_NTier_Task.BL.ServicesRepository.GenericRepositoryServices;
using RDP_NTier_Task.DAL.DTO.RequestDTO;
using RDP_NTier_Task.DAL.DTO.ResponseDTO;
using RDP_NTier_Task.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDP_NTier_Task.BL.ServicesRepository.BrandServices
{
    public interface IBrandServices : IGenericRepositoryServices<Brand,BrandRequest,BrandResponse> 
    {
        Task<int> Update(BrandUpdateRequest brand,int id);
    }
}
