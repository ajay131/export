using System.Reflection;
using Application.CQRS.Handler;
using Application.Interface;
using Infrastructure.Service.ConfigurationPage;
using Infrastructure.Service.ExportPage;
using Infrastructure.Service.ParameterMappingPage;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            // services.AddMediatR(typeof(DependencyInjection).Assembly);
            services.AddScoped<IMediator, Mediator>();

            // services.AddMediatR(Assembly.GetExecutingAssembly());
            // services.AddMediatR(typeof(GetAllDropdownDataQueryHandler).Assembly);

           services.AddScoped<IConfigScreen, ConfigScreen>();
            services.AddScoped<IParameterMappingScreen, ParameterMappingScreen>();
            services.AddScoped<IExportScreen, ExportScreen>();

            return services;
        }
    }
}