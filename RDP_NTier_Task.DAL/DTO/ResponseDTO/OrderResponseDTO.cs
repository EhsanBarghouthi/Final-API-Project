using RDP_NTier_Task.DAL.DTO.RequestDTO;
using RDP_NTier_Task.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDP_NTier_Task.DAL.DTO.ResponseDTO
{
    public class OrderResponseDTO
    {
        public string userId { get; set; }
        public int OrderId { get; set; }
        public orderStatusEnum status { get; set; } = orderStatusEnum.pending;
        public DateTime orderDate { get; set; } = DateTime.Now;
        public DateTime shippedDate { get; set; }
        public decimal totalPrice { get; set; }

        // payment 
        public PayMethod paymentMethod { get; set; }
        public string? paymentId { get; set; } // if visa 


        // carrier : 
        public string? carrierName { get; set; }
        public string? trackingNumber { get; set; }

    }
}
