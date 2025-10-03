using Microsoft.EntityFrameworkCore;
using RDP_NTier_Task.DAL.DATA;
using RDP_NTier_Task.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDP_NTier_Task.DAL.Repostry.OrderRepository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext context;

        public OrderRepository(ApplicationDbContext context) {
            this.context = context;
        }


        public async Task<Order?> GetUserByOrder(int orderId)
        {
            return await context.Orders.Include(u => u.user).FirstOrDefaultAsync(ord => ord.OrderId == orderId); 
        }


        public async Task<bool> addOrder(Order order)
        {
            await context.Orders.AddAsync(order);
            await context.SaveChangesAsync();
            return true; 
        }

        public async Task<bool> addOrderItem(List<OrderItem> orderItems)
        {
            await context.AddRangeAsync(orderItems);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Order>> GetOrderByUserId(string userId)
        {
            List<Order> orders = await context.Orders.Where(o=>o.userId==userId).ToListAsync();
            return orders;
        }

        public async Task<List<Order>> GetOrdersByStatus(orderStatusEnum status)
        {
            List<Order> orders = await context.Orders.Where(st=>st.status ==status).ToListAsync();
            return orders;
        }
        public async Task<bool> ChangeOrderStatus(int orderID, orderStatusEnum newStatus)
        {
            Order order = await context.Orders.FindAsync(orderID);
            if (order is null) return false;
            order.status = newStatus;
            var result = await context.SaveChangesAsync();
            if(result > 0 ) return true;
            return false;
        }

        // check approaved the user to review the product : user order and product is approved to user (so you can review)
        public async Task<bool> UserApprovedToReviewProduct(int productId, string userId)
        {
            bool result = await context.Orders.Include(ord => ord.orderItems)
                .AnyAsync(o => o.userId == userId && o.status == orderStatusEnum.completed && o.orderItems
                .Any(oi => oi.productId == productId));

            return result; 
        }
    }
}
