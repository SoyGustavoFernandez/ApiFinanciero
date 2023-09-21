using Microsoft.Extensions.DependencyInjection;
using RepositoryBackEnd;
using ServicesBackEnd;

namespace DependencyInjectionBackEnd
{
    public static class DependencyInjectionConfiguration
    {
        public static IServiceCollection ConfigureDependencyInjection(this IServiceCollection services)
        {
            services.AddScoped<IPersonaRepository, PersonaRepository>();
            services.AddScoped<IPersonaService, PersonaService>();

            services.AddScoped<IClienteRepository, ClienteRepository>();
            services.AddScoped<IClienteService, ClienteService>();

            services.AddScoped<ICuentaRepository, CuentaRepository>();
            services.AddScoped<ICuentaService, CuentaService>();

            return services;
        }
    }
}