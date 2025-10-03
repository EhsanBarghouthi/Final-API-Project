using RDP_NTier_Task.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDP_NTier_Task.DAL.Repostry.ReviewRepository
{
    public interface IReviewRepository
    {
        Task<bool> isAllowToReview(string userId, int productId);
        Task<bool> AddReview(Review review);
    }
}
