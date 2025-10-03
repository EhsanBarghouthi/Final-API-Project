using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using RDP_NTier_Task.BL.ServicesRepository.Authentication_Services;
using RDP_NTier_Task.DAL.DTO.ResponseDTO;
using RDP_NTier_Task.DAL.DTO.RequestDTO;

namespace RDP_NTier_Task.PL.Areas.Identity
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private readonly IAuthonticationServices authenticationServices;

        public IdentityController(IAuthonticationServices authenticationServices)
        {
            this.authenticationServices = authenticationServices;
        }

        [HttpPost("login")]
        public async Task<ActionResult<LoginResponse>> Login([FromBody] SignInRequest logginRequest)
        {

            var result = await authenticationServices.LoginAsync(logginRequest);

            return Ok(result);
        }
        [HttpPost("Register")]
        public async Task<ActionResult<string>> Register([FromBody] RegisterationRequest registerRequest)
        {
            RegistrationResponse result =await authenticationServices.Register(registerRequest);

            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Errors);   
        }

        [HttpGet("confirmEmail")]
        public async Task<ActionResult<string>> confirmEmail([FromQuery]string id , [FromQuery]string token)
        {
            bool result =await authenticationServices.confirmEmail(id ,token);
            if (result)
            {
                return Ok("Email Confirmed !!!! ");

            }
            return BadRequest("Email Not Confirmed !!! ");

        }

        [HttpPost("RequestRestPassword")]
        public async Task<IActionResult> requestPasswordReset(requestPasswordResetRequest requestReset)
        {
            bool request =await authenticationServices.requestResetPassword(requestReset.Email);
            if (request)
            {
                return Ok(); 
            }
            return BadRequest(); 

        }

        [HttpPost("ResetPassword")]
        public async Task<IActionResult> resetPassword(resetPasswordRequest resetPassword)
        {
            bool resetResult =await authenticationServices.resetPassword(resetPassword);
            if (resetResult)
            {
                return Ok("Password Reset Done !!!! ");
            }
            return BadRequest("Password Not Reset !! ");
        }

    }
}
