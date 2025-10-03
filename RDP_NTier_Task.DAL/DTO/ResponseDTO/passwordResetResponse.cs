using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDP_NTier_Task.DAL.DTO.ResponseDTO
{
    public class passwordResetResponse
    {
        public string newPassword { get; set; }
        public string confirmPassword { get; set; }
    }
}
