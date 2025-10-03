using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDP_NTier_Task.DAL.DTO.Configurations
{
    public class StripeConfig
    {
        public string SecretKey { get; set; }
        public string PublicKey { get;set; }
    }
}
