using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Application.Contract.Infrastructures;
using Ordering.Application.Contract.Persistences;
using Ordering.Infrastructure.Mail;
using Ordering.Infrastructure.Persistences;
using Ordering.Infrastructure.Persistences.Repositories;

namespace Ordering.Infrastructure
{
    public static class InfrustructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration) 
        {
            services.AddDbContext<OrderDbContext>(options=> options.UseSqlServer(configuration.GetConnectionString("OrderDb")));
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddTransient<IEmailService, EmailService>();
            return services;
        }
    }
}
