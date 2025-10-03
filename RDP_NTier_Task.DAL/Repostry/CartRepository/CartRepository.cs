using Microsoft.EntityFrameworkCore;
using RDP_NTier_Task.DAL.DATA;
using RDP_NTier_Task.DAL.DTO.ResponseDTO;
using RDP_NTier_Task.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDP_NTier_Task.DAL.Repostry.CartRepository
{
    public class CartRepository : ICartRepository
    {
        private readonly ApplicationDbContext context;

        public CartRepository(ApplicationDbContext context) 
        {
            this.context = context;
        }
        public async Task<int> AddCart(Cart cart)
        {
           await context.Carts.AddAsync(cart);
           int result = await context.SaveChangesAsync(); 
            return result;

        }

      
        public async Task<List<Cart>> GetItems(string userId)
        {
            List<Cart> responses = await context.Carts
                .Include(c => c.product)               // include product info
                .Where(c => c.userId == userId)       // filter by user
                .ToListAsync();
            
            return responses;
        }

        public async Task<int> DeleteItem(string userId, int productId)
        {
            var cartItem = await context.Carts
                .FirstOrDefaultAsync(r => r.userId == userId && r.productId == productId);

            if (cartItem != null)
            {
                context.Carts.Remove(cartItem);
                return await context.SaveChangesAsync();
            }

            return 0; 
        }
        public async Task<int> DeleteCartForUser(string userId)
        {
            var items = await context.Carts.Where(context => context.userId == userId).ToListAsync();
            context.Carts.RemoveRange(items);
            return await context.SaveChangesAsync();
        }

    }
}
