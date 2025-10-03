using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDP_NTier_Task.DAL.DTO.ResponseDTO
{
    public class userDTO
    {
        public string userId { get; set; }

        public string? PhoneNumber { get; set; }
        public string Email { get; set; }

        public string? UserName { get; set; }
        public string FullName { get; set; }
        public string? City { get; set; }
        public string roleName {  get; set; }
    }
}
