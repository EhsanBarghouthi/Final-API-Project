using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDP_NTier_Task.DAL.DTO.ResponseDTO
{
    public class CategoryResponse
    {
        public int Id { get; set; }
        public string categoryName { get; set; }
        public string? categoryDescription { get; set; }
    }
}
