using System;
using Basket.Entity;
using System.Linq.Expressions;

namespace Basket.Data
{
    public interface IRepository<T, in TKey> where T : class, IEntity<TKey>, new() where TKey : IEquatable<TKey>
    {
        Task<bool> AnyAsync();
        IQueryable<T> Get(Expression<Func<T, bool>> predicate = null);
        Task<T> GetByIdAsync(TKey id);
        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(TKey id, T entity);
        Task<T> DeleteAsync(T entity);
        Task<T> DeleteAsync(TKey id);
    }
}

