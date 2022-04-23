using ControlClimaApi.Aplication.Services;
using ControlClimaApi.Domain.Abstractions.CalculosEstadisticos;
using ControlClimaApi.Domain.Abstractions.IoTServices;
using ControlClimaApi.Domain.Abstractions.Repositories;
using ControlClimaApi.Domain.Abstractions.Services;
using ControlClimaApi.Infraestructure.CalculosEstadisticos;
using ControlClimaApi.Infraestructure.IoTServices;
using ControlClimaApi.Infraestructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlClimaApi.Infraestructure.Persistance
{
    public static class ServiceRegistration
    {
        public static void AddPersistanceInfraestructure(this IServiceCollection services)
        {
            // Repositorios
            services.AddScoped<IClimaRepository, ClimaRepository>();
            services.AddScoped<IUbicacionRepository, UbicacionRepository>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();

            // Servicios
            services.AddScoped<IClimaService, ClimaService>();
            services.AddScoped<IUbicacionService, UbicacionService>();
            services.AddScoped<IUsuarioService, UsuarioService>();

            // External
            services.AddScoped<ISensorClient, SensorClient>();
            services.AddScoped<ICalculadorEstadisticos, CalculadorEstadisticos>();

        }
    }
}
