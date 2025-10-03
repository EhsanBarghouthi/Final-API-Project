using RDP_NTier_Task.DAL.DTO.ResponseDTO;
using RDP_NTier_Task.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDP_NTier_Task.BL.OrderServices
{
    public interface IOrderServices
    {
        Task<List<OrderResponseDTO>> GetUserByStatus(orderStatusEnum status);
        Task<List<OrderResponseDTO>> GetOrderForUserById(string userID);
        Task<bool> ChangeOrderStatus(int orderID , orderStatusEnum newStatus);
    }
}
