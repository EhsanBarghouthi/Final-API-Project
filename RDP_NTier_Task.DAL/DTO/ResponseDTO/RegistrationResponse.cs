using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDP_NTier_Task.DAL.DTO.ResponseDTO
{
    public class RegistrationResponse
    {

        public bool Success { get; set; }
        public string Message { get; set; }
        public List<string> Errors { get; set; } = new();
    }
}

