using RDP_NTier_Task.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDP_NTier_Task.BL.ServicesRepository.GenericRepositoryServices
{
    public interface IGenericRepositoryServices<Tentity,Trequest,Tresponse> where Tentity : BaseModel
    {
        Task<List<Tresponse>> GetAll();
        Task<Tresponse> GetById(int id);
        Task<int> Delete(int id);
        Task<int> Update(Trequest entityRequest,int id);
        Task<int> Create(Trequest requestEntity);
        Task<int> RemoveAll();
        
        
    }
}
