using System.Collections.Generic;
using System.Threading.Tasks;
using OrderDomain.Entities;

namespace OrderDomain.Repositories
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task<IEnumerable<Order>> GetByUserName(string userName);
    }
}