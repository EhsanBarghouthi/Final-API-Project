using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDP_NTier_Task.DAL.DTO.ResponseDTO
{
    public class PaymentResponse
    {
        public bool success {  get; set; }
        public string message { get; set; }
        public string? URL { get; set; }
        public string? PaymentId { get; set; }

    }
}
