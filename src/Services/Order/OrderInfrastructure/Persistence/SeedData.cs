using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using OrderDomain.Entities;

namespace OrderInfrastructure.Persistence
{
    public class SeedData
    {
        public static async Task InitializeAsync(OrderContext context, ILogger<SeedData> logger)
        {
            if (!context.Orders.Any())
            {
                context.Orders.AddRange(GetPreconfiguredOrder());
                await context.SaveChangesAsync();
                logger.LogInformation("Seeded database associated with context {DbContextName}", typeof(OrderContext).Name);
            }
        }

        private static IEnumerable<Order> GetPreconfiguredOrder()
        {
            return new List<Order>
            {
                new Order
                {
                    UserName = "duoc",
                    TotalPrice = 100
                }
            };
        }
    }
}
