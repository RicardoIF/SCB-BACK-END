using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Contracts.Interfaces.RepositoryBase
{
    public interface IRepositoryBase<T>
    {
        IQueryable<T> FindAll(bool trackChanges, params Expression<Func<T, object>>[]? includeProperties);
        IQueryable<T> FindByCondition(
           Expression<Func<T, bool>> expression,
           bool trackChanges,
           Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
           Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null);
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
        void Deleted(T entity);
        Task<int> SaveAsync();
    }
}
