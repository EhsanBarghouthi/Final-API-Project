using RDP_NTier_Task.DAL.DTO.RequestDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace RDP_NTier_Task.DAL.Models
{
    public enum orderStatusEnum
    {
        pending = 1 ,cancelled = 2 , processeded = 3 , completed = 4
    }
    public class Order
    {
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
        
        public string userId { get; set; }
        public ApplicationUser user { get; set; }
        public List<OrderItem> orderItems { get; set; } = new List<OrderItem>();
    }
}
