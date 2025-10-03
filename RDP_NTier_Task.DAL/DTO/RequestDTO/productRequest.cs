using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDP_NTier_Task.DAL.DTO.RequestDTO
{
    public class productRequest
    {
        public string productName { get; set; }
        public string productCode { get; set; }
        public string productDescription { get; set; }
        public int quantity { get; set; }
        public double productPrice { get; set; }
        public double rate { get; set; }
        public IFormFile image { get; set; }
        public List<IFormFile> subImages { get; set; }
        public DateOnly expiredDate { get; set; }

        public int categoryId { get; set; }
        public int brandId { get; set; }
    }
}
