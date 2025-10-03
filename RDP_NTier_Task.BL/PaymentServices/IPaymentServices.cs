using Microsoft.AspNetCore.Http;
using RDP_NTier_Task.DAL.DTO.RequestDTO;
using RDP_NTier_Task.DAL.DTO.ResponseDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDP_NTier_Task.BL.PaymentServices
{
    public interface IPaymentServices
    {
        Task<PaymentResponse> PaymentProcess(PaymentRequest paymentRequest, string userId,HttpRequest httpRequest);
        Task<bool> HandlePaymentSuccess(int orderId);
        //Task<int> DeleteCartForUser(string userId);

    }
}
