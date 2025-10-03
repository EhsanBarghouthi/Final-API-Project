using Microsoft.EntityFrameworkCore;
using RDP_NTier_Task.DAL.DATA;
using RDP_NTier_Task.DAL.Models;
using RDP_NTier_Task.DAL.Repostry.GenericReporstry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDP_NTier_Task.DAL.Repostry.ProductRepository
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext context;

        public ProductRepository(ApplicationDbContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<int> decrementQuantity(int productId, int quantity)
        {
            var prod = await context.Products.FindAsync(productId);

            if (prod is null) throw new Exception("Product not found !! ");
            if (prod.quantity < quantity) throw new Exception("Product Quantity Not Enough !! ");
            prod.quantity -= quantity;
            // persist change
            await context.SaveChangesAsync();

            // return new quantity
            return prod.quantity;
        }

        public async Task<int> decrementQuantities(List<(int productId, int productQuantity)> productDecrement)
        {
            // Get all products needed in one query
            var productIds = productDecrement.Select(p => p.productId).ToList();
            var products = await context.Products
                .Where(p => productIds.Contains(p.Id))
                .ToListAsync();

            foreach (var item in productDecrement)
            {
                var theProduct = products.FirstOrDefault(p => p.Id == item.productId);

                if (theProduct is null)
                    throw new Exception($"Product {item.productId} not found!");

                if (theProduct.quantity < item.productQuantity)
                    throw new Exception($"Not enough stock for product {theProduct.Id}");

                theProduct.quantity -= item.productQuantity;
            }

            return await context.SaveChangesAsync();
        }
        public async Task<List<Product>> GetAllProducts()
        {
            return await context.Products.Include(p=>p.subImages).Include(rev=>rev.Reviews).ThenInclude(us=>us.User).ToListAsync();
        }


    }
}
