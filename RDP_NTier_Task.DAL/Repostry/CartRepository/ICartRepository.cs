using RDP_NTier_Task.DAL.DTO.ResponseDTO;
using RDP_NTier_Task.DAL.Models;
using RDP_NTier_Task.DAL.Repostry.GenericReporstry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDP_NTier_Task.DAL.Repostry.CartRepository
{
    public interface ICartRepository 
    {
        Task<int> AddCart(Cart cart);
        Task<List<Cart>> GetItems(string userId);
        Task<int> DeleteItem(string userId,int productId);
        Task<int> DeleteCartForUser(string userId);


    }
}
