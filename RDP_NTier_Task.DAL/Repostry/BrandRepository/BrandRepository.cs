using RDP_NTier_Task.DAL.DATA;
using RDP_NTier_Task.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RDP_NTier_Task.DAL.Repostry.GenericReporstry;

namespace RDP_NTier_Task.DAL.Repostry.BrandRepository
{
    public class BrandRepository : GenericRepository<Brand>, IBrandRepository
    {
        public BrandRepository(ApplicationDbContext context) : base(context)
        {
        
        }
    }
}
