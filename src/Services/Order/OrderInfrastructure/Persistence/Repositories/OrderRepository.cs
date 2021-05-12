using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OrderDomain.Entities;
using OrderDomain.Repositories;

namespace OrderInfrastructure.Persistence.Repositories
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(OrderContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Order>> GetByUserName(string userName)
        {
            return await _context.Orders
                .Where(o => o.UserName == userName)
                .ToListAsync();
        }
    }
}