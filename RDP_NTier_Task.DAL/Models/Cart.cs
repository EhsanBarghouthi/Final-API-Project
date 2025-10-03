using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDP_NTier_Task.DAL.Models
{
    [PrimaryKey(nameof(productId),nameof(userId))]
    public class Cart
    {

        public int quantity {  get; set; }
        public string userId {  get; set; }
        public int productId { get; set; }
        public ApplicationUser user { get; set; }
        public Product product { get; set; }


    }
}
