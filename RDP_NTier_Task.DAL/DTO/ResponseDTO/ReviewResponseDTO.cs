using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDP_NTier_Task.DAL.DTO.ResponseDTO
{
    public class ReviewResponseDTO
    {
        public int Id { get; set; }
        public int Rate { get; set; }
        public string userFullName { get; set; }
        public string? comment { get; set; }
        public DateTime ReviewDate { get; set; }
        public string Ordering { get; set; }   // it is for rate the review , for user have more prods for example . 

    }
}
