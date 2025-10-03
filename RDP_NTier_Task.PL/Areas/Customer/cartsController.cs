using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RDP_NTier_Task.BL.ServicesRepository.CartServices;
using RDP_NTier_Task.DAL.DTO.RequestDTO;
using RDP_NTier_Task.DAL.DTO.ResponseDTO;
using RDP_NTier_Task.DAL.Models;
using System.Security.Claims;

namespace RDP_NTier_Task.PL.Areas.Customer
{
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Area("Customer")]
    [Authorize]
    public class cartsController : ControllerBase
    {
        private readonly ICartServices cartServices;
        private readonly IHttpContextAccessor httpContext;

        public cartsController(ICartServices cartServices , IHttpContextAccessor httpContext)
        {
            this.cartServices = cartServices;
            this.httpContext = httpContext;
        }

        [HttpPost]
        public async Task<IActionResult> AddCart(cartRequest cartRequest)
        {
            string userId = httpContext.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            int result = await cartServices.AddCart(userId,1,cartRequest);
            if (result == 0) return BadRequest("Cant Add The Cart !!!! ");
            return Ok("Added The Cart Succefully !!!! ");
        }

        [HttpGet]
        public async Task<ActionResult<CartAllListResponse>> getCart()
        {
            string userId = httpContext.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            CartAllListResponse cartAll =await cartServices.GetAll(userId);
            if(cartAll == null) return BadRequest("Not Exist!!!!!!!!");
            return Ok(cartAll);
        }

        //[HttpDelete("{productId:int}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteItem([FromBody] cartRequest cartRequestDelete)
        {
            string userID = httpContext.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            int result =await cartServices.DeleteElement(userID,cartRequestDelete.productId);
            if (result == 0) return BadRequest("The Element Not Deleted ");
            return Ok("Deleted Success !!! ");
        }
    }
}
