using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace RDP_NTier_Task.DAL.DTO.ResponseDTO
{
    public class productResponse
    {
        public string productName { get; set; }
        public string productCode { get; set; }
        public string productDescription { get; set; }
        public int quantity { get; set; }
        public double productPrice { get; set; }
        public double rate { get; set; }
        public string image { get; set; }
        public DateOnly expiredDate { get; set; }

        public int categoryId { get; set; }
        public int brandId { get; set; }

        public string ImageUrl { get; set; }
        public List<string> subImages { get; set; } = new List<string>();

        // for review : 
        public List<ReviewResponseDTO> reviewResponse { get; set; } = new List<ReviewResponseDTO>();

       

    }
}
