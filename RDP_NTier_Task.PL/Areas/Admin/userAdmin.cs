using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using RDP_NTier_Task.BL.OrderServices;
using RDP_NTier_Task.BL.userServices;
using RDP_NTier_Task.DAL.DTO.ResponseDTO;
using RDP_NTier_Task.DAL.Models;
using Stripe;
using System.Runtime.InteropServices;

namespace RDP_NTier_Task.PL.Areas.Admin
{
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Area("Admin")]
    //[Authorize(Roles = "Admin")]
    public class userAdmin : ControllerBase
    {
        private readonly IUserServices userServices;
        private readonly IOrderServices orderServices;

        public userAdmin(IUserServices userServices , IOrderServices orderServices)
        {
            this.userServices = userServices;
            this.orderServices = orderServices;
        }

        [HttpGet("{userID}")]
        public async Task<ActionResult<userDTO>> GetUserById([FromRoute] string userID)
        {
            userDTO userById = await userServices.getUserById(userID);
            return Ok(userById);

        }

        [HttpGet]
        public async Task<ActionResult<List<userDTO>>> GetAllUsers()
        {
            var users = await userServices.getAllUsers();
            return Ok(users);

        }

        [HttpPatch("BlockUserById/{userID}")]
        public async Task<IActionResult> BlockUserById([FromRoute] string userID, [FromBody] int intervalInDaysToBlock)
        {
            var result = await userServices.BlockUserById(userID, intervalInDaysToBlock);

            if (!result)
                return NotFound("User not found or already blocked.");

            return Ok("User blocked successfully.");
        }

        [HttpPatch("unblock/{userID}")]
        public async Task<IActionResult> UnBlockUserById([FromRoute] string userId)
        {
            var result = await userServices.UnBlockUserById(userId);

            if (!result)
                return NotFound("User not found.");

            return Ok("User unblocked successfully.");
        }
        [HttpGet("isBlockedUser/{userID}")]
        public async Task<ActionResult<bool>> isBlockedUser([FromRoute] string userID)
        {
            var result = await userServices.isBlockUserById(userID);
            return Ok(result);

        }

        [HttpPatch("changeRole/{userID}")]
        public async Task<ActionResult<bool>> changeRole([FromRoute] string userID, [FromBody] string roleName)
        {
            var result = await userServices.changeRole(userID, roleName);
            return Ok(result);
        }

        [HttpGet("orderByStatus/{orderStatus}")]
        public async Task<ActionResult<List<OrderResponseDTO>>> GetOrderByStatus([FromRoute] orderStatusEnum orderStatus)
        {
            List<OrderResponseDTO> ordersDto = await orderServices.GetUserByStatus(orderStatus);
            return Ok(ordersDto);
        }

        [HttpGet("orderForUser/{userID}")]
        public async Task<ActionResult<List<OrderResponseDTO>>> GetOrderForUser([FromRoute] string userID)
        {
            List<OrderResponseDTO> ordersDto = await orderServices.GetOrderForUserById(userID);
            return Ok(ordersDto);
        }

        [HttpPatch("changeOrderStatus/{orderId}")]
        public async Task<ActionResult<bool>> changeOrderStatus([FromRoute] int orderId ,[FromBody]orderStatusEnum newStatus)
        {
            bool result = await orderServices.ChangeOrderStatus(orderId, newStatus);
            return Ok(result);
        }
}
}
