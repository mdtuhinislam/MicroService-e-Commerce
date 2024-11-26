using System.Reflection;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Ordering.Application
{
    public static class ApplicationServiceGegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            
                services.AddAutoMapper(Assembly.GetExecutingAssembly());
                services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
                services.AddMediatR(Assembly.GetExecutingAssembly());
                services.AddFluentValidationAutoValidation();
                return services;
        }
    }
}
