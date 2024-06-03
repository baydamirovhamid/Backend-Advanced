using Entity;
using Entity.Commons;
using Microsoft.EntityFrameworkCore;
using Repositories.Abstractions;
using Repositories.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Concrates
{
    public class EfGenericRepository<TEntity, TPrimaryKey> : IBaseRepository<TEntity, TPrimaryKey> where TEntity : BaseEntity<TPrimaryKey>
    {
       protected private readonly ProductDbContext DbContext;
       private readonly DbSet<TEntity> Table;


        public EfGenericRepository(ProductDbContext dbContext)
        {
            DbContext = dbContext;
            Table = dbContext.Set<TEntity>();         
        }
        public void Dispose()
        {

        }

        public async Task<bool> AddAsync(TEntity entity)
        {
            await Table.AddAsync(entity);
            return true;
        }

        public async  Task<bool>DeleteAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }

        IQueryable<TEntity> IBaseRepository<TEntity, TPrimaryKey>.FindQueryable() => Table.AsQueryable();
        async Task<TEntity> IBaseRepository<TEntity, TPrimaryKey>.Get(Expression<Func<TEntity, bool>> expression) => await Table.FirstOrDefaultAsync(expression);
        
        public async Task<List<TEntity>> GetAll()
        {
            return await Table.ToListAsync();
        }

       public async Task<bool> UpdateAsync(TEntity entity)
        {
            Table.Update(entity);
            return true;    
        }

        protected virtual void Dispose(bool disposing)
        {
          
        }

       

        void IDisposable.Dispose()
        {
           
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
