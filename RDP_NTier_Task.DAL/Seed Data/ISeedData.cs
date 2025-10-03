using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDP_NTier_Task.DAL.Seed_Data
{
    public interface ISeedData
    {
         Task DataSeedAsync();
        Task IdentitySeedAsync();

    }
}
