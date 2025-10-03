using Mapster;
using RDP_NTier_Task.DAL.DTO.ResponseDTO;
using RDP_NTier_Task.DAL.Models;
using RDP_NTier_Task.DAL.Repostry.OrderRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDP_NTier_Task.BL.OrderServices
{
    public class OrderServices : IOrderServices
    {
        private readonly IOrderRepository orderRepo;

        public OrderServices(IOrderRepository orderRepo)
        {
            this.orderRepo = orderRepo;
        }
        public async Task<List<OrderResponseDTO>> GetUserByStatus(orderStatusEnum status)
        {
            List<Order> orders = await orderRepo.GetOrdersByStatus(status);
            List<OrderResponseDTO> ordersDto = orders.Adapt<List<OrderResponseDTO>>();

            return ordersDto;
        }

        public async Task<List<OrderResponseDTO>> GetOrderForUserById(string userID)
        {
            List<Order> orders = await orderRepo.GetOrderByUserId(userID);
            List<OrderResponseDTO> ordersDto = orders.Adapt<List<OrderResponseDTO>>();
            return ordersDto;
        }

        public async Task<bool> ChangeOrderStatus(int orderID, orderStatusEnum newStatus)
        {
            bool result = await orderRepo.ChangeOrderStatus(orderID, newStatus);
            if (result) return true;
            return false; 
        }
    }
}
