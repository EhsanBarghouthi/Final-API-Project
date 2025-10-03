using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDP_NTier_Task.DAL.DTO.RequestDTO
{
    public class SignInRequest
    {
        public string Email { get; set; }

        public string Password { get; set; }

    }
}
