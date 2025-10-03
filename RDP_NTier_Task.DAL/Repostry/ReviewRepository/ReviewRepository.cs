using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RDP_NTier_Task.DAL.DATA;
using RDP_NTier_Task.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDP_NTier_Task.DAL.Repostry.ReviewRepository
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly ApplicationDbContext context;

        public ReviewRepository(ApplicationDbContext context) 
        {
            this.context = context;
        }
        public async Task<bool> isAllowToReview(string userId,int productId)
        {
            bool result =await context.Reviews.AnyAsync(rev => rev.UserId == userId && rev.ProductId == productId);
            return result;
        }
        public async Task<bool> AddReview(Review review)
        {
            if (review is null) return false;
            await context.Reviews.AddAsync(review);
            await context.SaveChangesAsync();
            return true;
        }
    }
}
