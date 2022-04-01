using System.Linq.Expressions;

namespace PollBack.Shared.Data
{
    public interface IRepositoryBase<T> where T : BaseEntity
    {
        Task<T?> GetAsync(Expression<Func<T, bool>> expression);
        Task<IEnumerable<T>> GetManyAsync();
        Task<IEnumerable<T>> GetManyAsync(Expression<Func<T, bool>> expression);
        Task<T> CreateAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task DeleteAsync(Expression<Func<T, bool>> expression);
    }
}
