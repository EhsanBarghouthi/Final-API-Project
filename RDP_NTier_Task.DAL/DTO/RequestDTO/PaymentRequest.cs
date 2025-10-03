using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDP_NTier_Task.DAL.DTO.RequestDTO
{
    public enum PayMethod
    {
        cash = 1 ,visa = 2
    }

    public class PaymentRequest
    {
        //public string userId { get; set; }
        public PayMethod PaymentMethod { get; set; }
    }
}
