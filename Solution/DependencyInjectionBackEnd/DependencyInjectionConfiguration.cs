using Microsoft.Extensions.DependencyInjection;
using RepositoryBackEnd.Cliente;
using RepositoryBackEnd.Cuenta;
using RepositoryBackEnd.Movimiento;
using RepositoryBackEnd.Persona;
using ServicesBackEnd.Cliente;
using ServicesBackEnd.Cuenta;
using ServicesBackEnd.Movimiento;
using ServicesBackEnd.Persona;
using ServicesBackEnd.Reporte;

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

            services.AddScoped<IMovimientoRepository, MovimientoRepository>();
            services.AddScoped<IMovimientoService, MovimientoService>();

            services.AddScoped<IReporteService, ReporteService>();

            return services;
        }
    }
}