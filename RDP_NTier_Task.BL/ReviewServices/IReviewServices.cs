using RDP_NTier_Task.DAL.DTO.RequestDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDP_NTier_Task.BL.ReviewServices
{
    public interface IReviewServices
    {
        Task<bool> AddReview(string userId,ReviewRequestDTO reviewRequestDTO);
    }
}
