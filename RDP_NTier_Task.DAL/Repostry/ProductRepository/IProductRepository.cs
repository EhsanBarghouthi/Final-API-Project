using RDP_NTier_Task.DAL.Models;
using RDP_NTier_Task.DAL.Repostry.GenericReporstry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDP_NTier_Task.DAL.Repostry.ProductRepository
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<int> decrementQuantity(int productId, int quantity);
        Task<int> decrementQuantities(List<(int productId , int productQuantity)> productDecrement);
        Task<List<Product>> GetAllProducts();
    }
}
