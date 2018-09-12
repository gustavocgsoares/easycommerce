using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Easy.Commerce.Domain.Shared;

namespace Easy.Commerce.Domain.Interfaces.Repositories
{
    public interface IRepository<TEntity, in TId>
        where TEntity : Entity<TEntity, TId>
    {
        Task<ServiceResponse<TEntity>> SaveAsync(TEntity entity);

        Task<ServiceResponse<TEntity>> GetAsync(TId id);

        Task<ServiceResponse<IEnumerable<TEntity>>> GetAsync(Expression<Func<TEntity, bool>> filter);

        Task<ServiceResponse<bool>> DeleteAsync(TId id);

        Task<ServiceResponse<bool>> DeleteAsync(Expression<Func<TEntity, bool>> filter);
    }
}