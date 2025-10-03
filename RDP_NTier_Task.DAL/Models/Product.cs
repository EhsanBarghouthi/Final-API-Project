using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDP_NTier_Task.DAL.Models
{
    public class Product : BaseModel
    {
        public string productName { get; set; }
        public string productCode { get; set; }
        public string productDescription { get; set; }
        public int quantity { get; set; }
        public double productPrice { get; set; }
        public double rate { get; set; }
        public DateOnly expiredDate { get; set; }
        public string image { get; set; }

        public int categoryId { get; set; }
        public Category category { get; set; }

        public int brandId { get; set; }
        public Brand brand { get; set; }

        public List<ProductImages> subImages { get; set; } = new List<ProductImages>();
        public List<Review> Reviews { get; set; } = new List<Review>();
    }

}
