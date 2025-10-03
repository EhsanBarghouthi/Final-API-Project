using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDP_NTier_Task.DAL.DTO.RequestDTO
{
    public class BrandUpdateRequest
    {
        public string BrandName { get; set; }
        public IFormFile? BrandImage { get; set; }
    }
}
