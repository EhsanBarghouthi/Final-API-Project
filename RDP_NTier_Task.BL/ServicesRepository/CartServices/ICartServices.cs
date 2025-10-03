using RDP_NTier_Task.DAL.DTO.RequestDTO;
using RDP_NTier_Task.DAL.DTO.ResponseDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDP_NTier_Task.BL.ServicesRepository.CartServices
{
    public interface ICartServices
    {
        Task<int> AddCart(string userId,int quantity ,cartRequest cart);
        Task<CartAllListResponse> GetAll(string userId);
        Task<int> DeleteElement(string userId,int productId);
    }
}
