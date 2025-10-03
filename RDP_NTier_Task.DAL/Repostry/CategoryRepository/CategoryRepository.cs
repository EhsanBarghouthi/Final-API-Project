using RDP_NTier_Task.DAL.DATA;
using RDP_NTier_Task.DAL.Models;
using RDP_NTier_Task.DAL.Repostry.GenericReporstry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDP_NTier_Task.DAL.Repostry.CategoryRepostry
{
    // Implementation
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(ApplicationDbContext context) : base(context)
        {
            // The GenericRepository already handles CRUD
            // You can add any extra methods here
        }
    }
}
