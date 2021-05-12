using System.Collections.Generic;
using System.Threading.Tasks;
using OrderDomain.Common;

namespace OrderDomain.Repositories
{
    public interface IRepository<T> where T : IAggregateRoot
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByAsync(int id);
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
    }
}