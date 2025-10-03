using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RDP_NTier_Task.BL.ReviewServices;
using RDP_NTier_Task.DAL.DTO.RequestDTO;
using System.Security.Claims;

namespace RDP_NTier_Task.PL.Areas.Customer
{
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Area("Customer")]
    [Authorize(Roles = "Customer")]
    public class ReviewsController : ControllerBase
    {
        private readonly IReviewServices reviewServices;

        public ReviewsController(IReviewServices reviewServices)
        {
            this.reviewServices = reviewServices;
        }
        [HttpPost("AddReview")]
        public async Task<ActionResult<bool>> AddReview([FromBody]ReviewRequestDTO reviewDTO)
        {
            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            bool result = await reviewServices.AddReview(user, reviewDTO);
            return Ok(result);

        }
    }
}
