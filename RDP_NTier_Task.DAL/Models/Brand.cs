using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDP_NTier_Task.DAL.Models
{
    public class Brand :BaseModel
    {
        public string BrandName { get; set; }
        public string BrandImage { get; set; }

        public ICollection<Product> Products { get; set; }=new List<Product>();
    }
}
