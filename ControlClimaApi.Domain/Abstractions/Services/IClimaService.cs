using ControlClimaApi.Domain.Entities;
using ControlClimaApi.Domain.Entities.Reports;
using ControlClimaApi.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlClimaApi.Domain.Abstractions.Services
{
    public interface IClimaService
    {
        /**
         * Consultas
         */
        List<Clima> ObtenerClimasPorUsuario(int idUsuario);
        List<Clima> ObtenerClimasDetalle();
        Clima ObtenerClimaDetalle(int id);
        List<Clima> ObtenerClimasPorUbicacion(int idUbicacion);
        List<Clima> ObtenerClimasFormulario(int idUsuario, DateTime fechaInicio, DateTime fechaFin, int? idUbicacion = null);
        List<ReporteClima> ObtenerClimaReporte(int idUbicacion, DateTime fechaInicio, DateTime fechaFin);

        /**
         * Registros
         */
        Clima RegistrarClima(Clima clima);

        /**
         * Actualizaciones
         */

        /**
         * Eliminaciones
         */
        int EliminarClima(int id);

        /**
         * Sensores 
         */
        Task<Clima?> ObtenerDatosSensorClima(int idUbicacion);
    }
}
