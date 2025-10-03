using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RDP_NTier_Task.DAL.DTO.ResponseDTO;
using RDP_NTier_Task.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDP_NTier_Task.BL.userServices
{
    public class UserServices : IUserServices
    {
        private readonly UserManager<ApplicationUser> userManager;

        public UserServices(UserManager<ApplicationUser> userManager ) 
        {
            this.userManager = userManager;
        }

      

        public async Task<List<userDTO>> getAllUsers()
        {
            List<ApplicationUser> allUsers = await userManager.Users.ToListAsync();

            List<userDTO> allUsersDTO=new List<userDTO>();

            foreach (var user in allUsers) 
            {
                var role = await userManager.GetRolesAsync(user);
                 allUsersDTO.Add(new userDTO
                {
                    userId = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    FullName = user.FullName,
                    PhoneNumber = user.PhoneNumber,
                    City = user.City,
                    roleName = role.FirstOrDefault()
                });
            }
            return allUsersDTO;
        }

        public async Task<userDTO> getUserById(string userID)
        {
            ApplicationUser userDetails = await userManager.FindByIdAsync(userID);
            if (userDetails == null) return null; 
            userDTO userDTO = userDetails.Adapt<userDTO>();
            return userDTO;

        }

        public async Task<bool> BlockUserById(string userID, int intervalToBlockInDays)
        {
            ApplicationUser user = await userManager.FindByIdAsync(userID);
            if (user == null) return false;
          
                user.LockoutEnabled = true;
                user.LockoutEnd = DateTime.UtcNow.AddDays(intervalToBlockInDays);

            var result = await userManager.UpdateAsync(user);
            return result.Succeeded;

        }
        public async Task<bool> UnBlockUserById(string userID)
        {
            ApplicationUser user = await userManager.FindByIdAsync(userID);
            if (user == null) return false;

            user.LockoutEnd = null;         // Remove lockout immediately
            user.LockoutEnabled = false;    // (Optional) disable locking mechanism for future

            var result = await userManager.UpdateAsync(user);
            return result.Succeeded;

        }
        public async Task<bool> isBlockUserById(string userID)
        {
            ApplicationUser user = await userManager.FindByIdAsync(userID);
            if (user == null) return false;

            if (user.LockoutEnd.HasValue && user.LockoutEnd > DateTime.UtcNow)
                return true;
            return false; 
        }

        public async Task<bool> changeRole(string userID, string roleName)
        {
            ApplicationUser user = await userManager.FindByIdAsync(userID);
            if (user is null) return false;

            var result = await userManager.AddToRoleAsync(user, roleName);
            
            return result.Succeeded;
        }

    
    }
}
