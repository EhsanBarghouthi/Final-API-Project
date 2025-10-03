using RDP_NTier_Task.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDP_NTier_Task.DAL.DTO.RequestDTO
{
    public class ReviewRequestDTO
    {
        public int ProductId { get; set; }
        public int Rate { get; set; }
        public string? comment { get; set; }

    }
}
