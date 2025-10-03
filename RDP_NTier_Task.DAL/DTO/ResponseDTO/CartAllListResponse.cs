using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDP_NTier_Task.DAL.DTO.ResponseDTO
{
    public class CartAllListResponse
    {
        public string userId { get; set; }
        public List<cartProductResponse> cartProducts { get; set; } = new List<cartProductResponse>();
        public decimal totalPriceForCart => cartProducts.Sum(i=>i.totalPrice); 
    }
}
