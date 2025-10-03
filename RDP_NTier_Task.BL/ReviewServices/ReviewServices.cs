using Microsoft.EntityFrameworkCore;
using RDP_NTier_Task.DAL.DTO.RequestDTO;
using RDP_NTier_Task.DAL.Models;
using RDP_NTier_Task.DAL.Repostry.OrderRepository;
using RDP_NTier_Task.DAL.Repostry.ReviewRepository;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDP_NTier_Task.BL.ReviewServices
{
    public class ReviewServices : IReviewServices
    {
        private readonly IReviewRepository reviewRepo;
        private readonly IOrderRepository orderRepo;

        public ReviewServices(IReviewRepository reviewRepo ,IOrderRepository orderRepo) 
        {
            this.reviewRepo = reviewRepo;
            this.orderRepo = orderRepo;
        }
        public async Task<bool> AddReview(string userId, ReviewRequestDTO reviewRequestDTO)
        {
            // check the user if have order that make or take the product : 
            // productId and UserId --> check in Order . (make it in the repo for order not need to service)
            bool result = await orderRepo.UserApprovedToReviewProduct(reviewRequestDTO.ProductId, userId);
            if (!result) return false;

            // check if it is available to make comment : (if not allowed to review more than one)
            bool approavedToReview = await reviewRepo.isAllowToReview(userId,reviewRequestDTO.ProductId);
            if (approavedToReview) return false;

            // 3. Map DTO to entity
            var review = reviewRequestDTO.Adapt<Review>();
            review.UserId = userId;     
            review.ReviewDate = DateTime.UtcNow; // (if you have timestamp)
            review.Ordering = "1";  

            // 4. Save to repository
            return await reviewRepo.AddReview(review);

        }
    }
}
