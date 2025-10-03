using RDP_NTier_Task.DAL.DATA;
using RDP_NTier_Task.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDP_NTier_Task.DAL.Repostry.GenericReporstry
{
    public interface IGenericRepository<Tentity> where Tentity : BaseModel
    {
        Task<Tentity> GetById(int id);
        Task<List<Tentity>> GetAll();
        Task<int> Create(Tentity entity);
        Task<int> Update(Tentity entity);
        Task<int> Delete(int id);
        Task<int> RemoveAll();

        
    }
}
