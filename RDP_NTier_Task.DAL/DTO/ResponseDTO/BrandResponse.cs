using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDP_NTier_Task.DAL.DTO.ResponseDTO
{
    public class BrandResponse
    {
        public string BrandName { get; set; }
        public string BrandImage { get; set; }
        // Computed property
        public string ImageUrl
        {
            get
            {
                if (string.IsNullOrEmpty(BrandImage))
                    return null;

                // Assuming images are served from /images folder in wwwroot
                return $"http://localhost:5164/wwwroot/images/{BrandImage}";
            }
        }
    }
}
