using Azure;
using Mapster;
using RDP_NTier_Task.DAL.DTO.CategoryDTO;
using RDP_NTier_Task.DAL.Models;
using RDP_NTier_Task.DAL.Repostry.CategoryRepostry;
using RDP_NTier_Task.DAL.Repostry.GenericReporstry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDP_NTier_Task.BL.ServicesRepository.GenericRepositoryServices
{
    public class GenericRepositoryServices<Tentity, Trequest, Tresponse> : IGenericRepositoryServices<Tentity, Trequest, Tresponse> where Tentity : BaseModel
    {
        private readonly IGenericRepository<Tentity> repository;

        public GenericRepositoryServices(IGenericRepository<Tentity> repository)
        {
            this.repository = repository;
        }

        public async Task<int> Create(Trequest Trequest)
        {

            Tentity entity = Trequest.Adapt<Tentity>();
            int created =await repository.Create(entity);
            return created;
        }

        public async Task<int> Delete(int id)
        {
            int deleted =await repository.Delete(id);
            return deleted;
        }

        public async Task<List<Tresponse>> GetAll()
        {
            List<Tentity> listAll =await repository.GetAll();
            List<Tresponse> TresponseList = listAll.Adapt<List<Tresponse>>();
            return TresponseList;
        }

        public async Task<Tresponse> GetById(int id)
        {
            Tentity entity =await repository.GetById(id);
            Tresponse entitytResponse = entity.Adapt<Tresponse>();
            return entitytResponse;
        }

        public async Task<int> RemoveAll()
        {
            int removedAll = await repository.RemoveAll();
            return removedAll;
        }

        public async Task<int> Update(Trequest entityRequest, int id)
        {
            var entity = await repository.GetById(id);

            if (entity is null) return 0; 

            var updatedEntity = entityRequest.Adapt(entity); // this to convert (i dont know what updated) .

            return await repository.Update(updatedEntity);                                               
        }
    }
}
