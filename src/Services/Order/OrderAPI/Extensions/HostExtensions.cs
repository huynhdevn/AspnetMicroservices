using System;
using System.Threading;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OrderInfrastructure.Persistence;

namespace Extensions
{
    public static class HostExtensions
    {
        public static IHost MigrateDatabase<TContext>(this IHost host, Action<OrderContext, IServiceProvider> seeder = null, int retry = 0)
        {
            using var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider;
            var logger = services.GetRequiredService<ILogger<TContext>>();
            var context = services.GetRequiredService<OrderContext>();

            try
            {
                InvokeSeeder(seeder, services, context);
                logger.LogInformation("Migrated database asscociated with context {DbContextName}", typeof(TContext).Name);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);

                if (retry < 10)
                {
                    retry++;
                    Thread.Sleep(2000);
                    MigrateDatabase<TContext>(host, seeder, retry);
                }
            }

            return host;
        }

        private static void InvokeSeeder(Action<OrderContext, IServiceProvider> seeder,
            IServiceProvider services, OrderContext context)
        {
            context.Database.EnsureCreated();
            if (seeder != null)
            {
                seeder(context, services);
            }
        }
    }
}