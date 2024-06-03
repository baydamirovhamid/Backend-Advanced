using Entity.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Abstractions
{
    public interface IBaseRepository<TEntity, in TPrimaryKey>: IDisposable 
        where TEntity : BaseEntity<TPrimaryKey>
    {
        IQueryable<TEntity> FindQueryable();

        Task<List<TEntity>> GetAll();
        Task<TEntity> Get(Expression<Func<TEntity, bool>> expression);
        Task<bool> AddAsync(TEntity entity);
        Task<bool> DeleteAsync(TEntity entity);
        Task<bool> UpdateAsync(TEntity entity);
        



    }
}
