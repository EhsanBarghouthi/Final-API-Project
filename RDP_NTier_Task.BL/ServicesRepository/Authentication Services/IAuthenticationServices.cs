using RDP_NTier_Task.DAL.DTO.RequestDTO;
using RDP_NTier_Task.DAL.DTO.ResponseDTO;
using RDP_NTier_Task.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDP_NTier_Task.BL.ServicesRepository.Authentication_Services
{
    public interface IAuthonticationServices
    {
        Task<RegistrationResponse> Register(RegisterationRequest registerRequest);
        Task<LoginResponse> LoginAsync(SignInRequest loginRequest);
        Task<bool> confirmEmail(string userId, string token);
        Task<bool> requestResetPassword(string email);
        Task<bool> resetPassword(resetPasswordRequest passwordRequest);


    }
}
