using RDP_NTier_Task.DAL.DTO.ResponseDTO;
using RDP_NTier_Task.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDP_NTier_Task.BL.userServices
{
    public interface IUserServices
    {
        Task<userDTO> getUserById(string userID);
        Task<List<userDTO>> getAllUsers();
        Task<bool> BlockUserById(string userID,int intervalToBlockInDays);
        Task<bool> UnBlockUserById(string userID);
        Task<bool> isBlockUserById(string userID); 

        Task<bool> changeRole(string userID,string roleName);
        



    }
}

