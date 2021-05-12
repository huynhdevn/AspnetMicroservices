using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OrderDomain.Common;
using OrderDomain.Repositories;

namespace OrderInfrastructure.Persistence.Repositories
{
    public class Repository<T> : IRepository<T> where T : EntityBase, IAggregateRoot
    {
        protected OrderContext _context;

        public Repository(OrderContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> GetByAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<T> AddAsync(T entity)
        {
            _context.Set<T>().Add(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task UpdateAsync(T entity)
        {
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}