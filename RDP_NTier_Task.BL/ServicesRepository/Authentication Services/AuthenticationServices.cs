using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RDP_NTier_Task.BL.General_Services;
using RDP_NTier_Task.DAL.DATA;
using RDP_NTier_Task.DAL.DTO.RequestDTO;
using RDP_NTier_Task.DAL.DTO.ResponseDTO;
using RDP_NTier_Task.DAL.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.Intrinsics.X86;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace RDP_NTier_Task.BL.ServicesRepository.Authentication_Services
{
    public class AuthenticationServices : IAuthonticationServices
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IEmailSender emailSender;
        private readonly IConfiguration configuration;

        public AuthenticationServices(UserManager<ApplicationUser> userManager, IEmailSender emailSender,IConfiguration configuration)
        {
            this.userManager = userManager;
            this.emailSender = emailSender;
            this.configuration = configuration;
        }


        public async Task<LoginResponse> LoginAsync(SignInRequest signInRequest)
        {
            var user = await userManager.FindByEmailAsync(signInRequest.Email);
            if (user.EmailConfirmed)
            {
                if (user is null)
                {
                    throw new Exception("The Email Not Exist ! Please Check It Or Make Register before ");
                }
                // to check if it is locked or not : if you need to auto check use SignInManager . 
                if (user.LockoutEnabled && user.LockoutEnd.HasValue && user.LockoutEnd.Value > DateTime.UtcNow)
                {
                    return new LoginResponse() { Token = "User is locked until " + user.LockoutEnd.Value.ToString("u") };
                }

                var result = await userManager.CheckPasswordAsync(user, signInRequest.Password);
                if (result)
                {
                    var roles = await userManager.GetRolesAsync(user);
                    var token = GenerateToken(user, roles);

                    LoginResponse loginResponse = new LoginResponse() { Token = token };
                    // loged in 
                    return loginResponse;
                }
                else
                    return new LoginResponse() { Token = "The Log In Failed " };
            }
            else
                return new LoginResponse() { Token = "Email Not Confirmed !!!!! " };
        }



        public async Task<RegistrationResponse> Register(RegisterationRequest registerRequest)
        {
            ApplicationUser user3 = new ApplicationUser
            {
                UserName = registerRequest.UserName,
                Email = registerRequest.Email,
                FullName = registerRequest.FullName,
                PhoneNumber = registerRequest.PhoneNumber
            };




            RegistrationResponse response = new RegistrationResponse();


            var result = await userManager.CreateAsync(user3, registerRequest.Password);
            //await userManager.AddToRoleAsync(user3, "SuperAdmin");


            if (user3 is null) throw new Exception("Cant Make the Registration");

            if (result.Succeeded)
            {
                response.Success = true;
                response.Message = "User registered successfully!";

                string token = await userManager.GenerateEmailConfirmationTokenAsync(user3);
                string url = $"http://localhost:5164/api/Identity/confirmEmail?id={user3.Id}&token={Uri.EscapeDataString(token)}";

                string body = $"Hello {user3.UserName},<br/>" +
                  $"<a href=\"{url}\">Confirm</a><br/>" +
                  "Thanks for registering!";

                // ✅ Send Welcome Email
                await emailSender.SendEmailAsync("adaghara10@gmail.com", "Welcome!", body);


                return response;
            }

            response.Success = false;
            response.Errors = result.Errors.Select(e => e.Description).ToList();
            return response;
        }


        public async Task<bool> confirmEmail(string userId, string token)
        {
            var user = await userManager.FindByIdAsync(userId);
            var result = await userManager.ConfirmEmailAsync(user, token);
            //user.EmailConfirmed = true;
            if (result.Succeeded)
            {
                return true;
            }
            else
                return false;
        }

        private string GenerateToken(ApplicationUser user, IList<string> roles)
        {
            var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(ClaimTypes.Name, user.UserName)
        };

            // Add roles as claims
            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                //issuer: _config["Jwt:Issuer"],
                //audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(double.Parse(configuration["Jwt:ExpireMinutes"])),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<bool> requestResetPassword(string email)
        {
            Random generator = new Random();
            var user =await userManager.FindByEmailAsync(email);
            if (user is not null)
            {
                // Generate a secure 6-digit OTP
                var rng = new System.Security.Cryptography.RNGCryptoServiceProvider();
                byte[] bytes = new byte[4];
                rng.GetBytes(bytes);
                int otp = BitConverter.ToInt32(bytes, 0) % 1000000;
                if (otp < 0) otp = -otp; // ensure positive
                string code = otp.ToString("D6");

                user.PasswordCode = code;
                user.CodeExpiredTime = DateTime.Now.AddMinutes(5);

                // Persist changes
                await userManager.UpdateAsync(user);

                string body = $"Hello {user.UserName},<br/>" +
                              $"Your code to reset password: {code}<br/>" +
                              "It expires in 5 minutes.";

                // Send email
                await emailSender.SendEmailAsync("adaghara10@gmail.com", "Password Reset Request", body);

                return true;
            }
            return false; 
        }
        public async Task<bool> resetPassword(resetPasswordRequest passwordRequest)
        {
            var user = await userManager.FindByEmailAsync(passwordRequest.Email);
            if (user is null) return false;

            // Check if OTP/Code is expired
            if (user.CodeExpiredTime < DateTime.Now)
                return false;

            // Check if code matches
            if (user.PasswordCode != passwordRequest.PasswordCode)
                return false;

            // Check if passwords match
            if (passwordRequest.newPassword != passwordRequest.confirmNewPassword)
                return false;

            // Generate Identity reset token
            var token = await userManager.GeneratePasswordResetTokenAsync(user);

            // Reset password
            var result = await userManager.ResetPasswordAsync(user, token, passwordRequest.newPassword);

            return result.Succeeded;
        }
    }
}
