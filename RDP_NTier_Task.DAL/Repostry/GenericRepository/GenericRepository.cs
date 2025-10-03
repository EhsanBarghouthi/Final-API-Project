using RDP_NTier_Task.DAL.DATA;
using RDP_NTier_Task.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDP_NTier_Task.DAL.Repostry.GenericReporstry
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseModel
    {
        private readonly ApplicationDbContext context;

        public GenericRepository(ApplicationDbContext context) {
            this.context = context;
        }
        public async Task<int> Create(T entity)
        {
            context.Add(entity);
            return context.SaveChanges();
        }

        public async Task<int> Delete(int id)
        {
            var entity = context.Set<T>().Find(id);
            context.Remove(entity);
            return context.SaveChanges();
        }

        public async Task<List<T>> GetAll()
        {
            List<T> list = context.Set<T>().ToList();
            return list;
        }

        public async Task<T> GetById(int id)
        {
            T entity = context.Set<T>().Find(id);
            return entity;
        }

        public async Task<int> Update(T entity)
        {
            context.Set<T>().Update(entity);
            return context.SaveChanges();
        }

        public async Task<int> RemoveAll()
        {
            // Get all entities first
            var allEntities = context.Set<T>().ToList();

            // Remove them
            context.Set<T>().RemoveRange(allEntities);

            // Save changes
            return context.SaveChanges();
        }
    }

}
