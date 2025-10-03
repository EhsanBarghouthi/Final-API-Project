using Azure;
using Microsoft.AspNetCore.Http;
using RDP_NTier_Task.BL.ServicesRepository.GenericRepositoryServices;
using RDP_NTier_Task.DAL.DTO.RequestDTO;
using RDP_NTier_Task.DAL.DTO.ResponseDTO;
using RDP_NTier_Task.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDP_NTier_Task.BL.ServicesRepository.ProductServices
{
    public interface IProductServices : IGenericRepositoryServices<Product, productRequest, productResponse>
    {
        Task<int> Update(ProductUpdateRequest updateRequest, int id);
        Task<List<productResponse>> GetAllProducts(HttpRequest request, bool onlyActive = false);


    }
}
