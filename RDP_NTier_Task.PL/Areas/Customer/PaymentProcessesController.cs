using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RDP_NTier_Task.BL.PaymentServices;
using RDP_NTier_Task.DAL.DTO.RequestDTO;
using RDP_NTier_Task.DAL.DTO.ResponseDTO;
using Stripe.Climate;
using System.Security.Claims;

namespace RDP_NTier_Task.PL.Areas.Customer
{
    [Route("api/[Area]/[controller]")]
    [ApiController]
    [Area("Customer")]
    [Authorize]
    public class PaymentProcessesController : ControllerBase
    {
        private readonly IPaymentServices paymentServices;

        public PaymentProcessesController(IPaymentServices paymentServices)
        {
            this.paymentServices = paymentServices;
        }
        [HttpPost]
        public async Task<IActionResult> paymentProcess([FromBody]PaymentRequest paymentRequest)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null) return BadRequest("User Not Exist !!!!!");

            PaymentResponse paymentResponse =await paymentServices.PaymentProcess(paymentRequest, userId, Request);
            if (paymentResponse.success)
                return Ok(paymentResponse);
            else return BadRequest(paymentResponse.message);

        }

        [HttpGet("Success/{orderId}")]
        [AllowAnonymous]
        public async Task<IActionResult> Success([FromRoute] int orderId)
        {
            var result = await paymentServices.HandlePaymentSuccess(orderId);
            return Ok("Success!!!!!");
        }
    }   
}
