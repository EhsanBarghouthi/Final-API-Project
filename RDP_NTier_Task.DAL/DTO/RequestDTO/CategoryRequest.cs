using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDP_NTier_Task.DAL.DTO.CategoryDTO
{
    public class CategoryRequest
    {
        public string categoryName { get; set; }
        public string? categoryDescription { get; set; }
    }
}
