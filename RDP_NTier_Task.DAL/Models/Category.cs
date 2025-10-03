using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDP_NTier_Task.DAL.Models
{
    public class Category :BaseModel
    {
        public string categoryName { get; set; }
        public string? categoryDescription { get; set; }

        public ICollection<Product> products { get; set; } = new List<Product>();

    }
}
