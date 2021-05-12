using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OrderApplication.Contracts;
using OrderApplication.Models;
using OrderDomain.Repositories;
using OrderInfrastructure.Mail;
using OrderInfrastructure.Persistence;
using OrderInfrastructure.Persistence.Repositories;

namespace OrderInfrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<OrderContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("OrderingConnectionString")));

            services.AddScoped<IOrderRepository, OrderRepository>();

            services.Configure<EmailSettings>(c => configuration.GetSection("EmailSettings"));
            services.AddTransient<IEmailService, EmailService>();

            return services;
        }

    }
}