using RDP_NTier_Task.DAL.DTO.ResponseDTO;
using RDP_NTier_Task.DAL.Models;
using RDP_NTier_Task.DAL.Repostry.GenericReporstry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDP_NTier_Task.DAL.Repostry.OrderRepository
{
    public interface IOrderRepository
    {
        Task<Order> GetUserByOrder(int orderId);
        Task<bool> addOrder(Order order);
        Task<bool> addOrderItem(List<OrderItem> orderItems);
        Task<List<Order>> GetOrderByUserId(string userId);
        Task<List<Order>> GetOrdersByStatus(orderStatusEnum status);
        Task<bool> ChangeOrderStatus(int orderID, orderStatusEnum newStatus);
        Task<bool> UserApprovedToReviewProduct(int productId, string userId);

    }
}
