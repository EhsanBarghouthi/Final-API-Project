using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDP_NTier_Task.DAL.DTO.RequestDTO
{
    public class resetPasswordRequest
    {
        public string Email { get; set; }
        public string PasswordCode { get; set; }
        public string newPassword { get; set; }
        public string confirmNewPassword { get; set; }
    }
}
