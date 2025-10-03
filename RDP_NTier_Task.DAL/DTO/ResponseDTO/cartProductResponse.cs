using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDP_NTier_Task.DAL.DTO.ResponseDTO
{
    public class cartProductResponse
    {
        public int productId { get; set; }
        public int quantity { get; set; }
        public string productName { get; set; }
        public decimal productPrice { get; set; }
        public decimal totalPrice =>productPrice*quantity;

    }
}
