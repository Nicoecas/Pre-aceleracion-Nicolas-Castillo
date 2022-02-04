using Microsoft.EntityFrameworkCore;
using PreAceleracion.Data.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PreAceleracion.Data
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly PreAceleracionContext preAceleracionContext;
        public GenericRepository(PreAceleracionContext preAceleracionContext)
        {
            this.preAceleracionContext = preAceleracionContext;
        }

        //Delete and Get generic
        public async Task<TEntity> Delete(int id)
        {
            var entity = await preAceleracionContext.Set<TEntity>().FindAsync(id);
            if (entity == null)
            {
                return entity;
            }
            preAceleracionContext.Set<TEntity>().Remove(entity);
            await preAceleracionContext.SaveChangesAsync();
            return entity;
        }

        public async Task<List<TEntity>> GetAll()
        {
            return await preAceleracionContext.Set<TEntity>().ToListAsync();
        }

        public async Task<TEntity> GetById(int id)
        {
            return await preAceleracionContext.Set<TEntity>().FindAsync(id);
        }
    }
}