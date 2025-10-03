using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDP_NTier_Task.DAL.Models
{
    [PrimaryKey(nameof(orderId), nameof(productId))]
    public class OrderItem
    {
        public int productId { get; set; }
        public int orderId { get; set; }

        public decimal totalPrice { get; set; } // for product
        public Order order { get; set; }        
        public Product product { get; set; }
    }
}
