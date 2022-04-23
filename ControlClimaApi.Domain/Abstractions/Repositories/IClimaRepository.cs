using ControlClimaApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlClimaApi.Domain.Abstractions.Repositories
{
    public interface IClimaRepository
    {
        /**
         * Consultas
         */
        List<Clima> ObtenerClimasPorUsuario(int idUsuario);
        List<Clima> ObtenerClimasDetalle(int? id);
        List<Clima> ObtenerClimasPorUbicacion(int idUbicacion);
        List<Clima> ObtenerClimasFormulario(int idUsuario, DateTime fechaInicio, DateTime fechaFin, int? idUbicacion = null);
        List<Clima> ObtenerFuenteClimaReporte(int idUbicacion, DateTime fechaInicio, DateTime fechaFin);

        /**
         * Registros
         */
        Clima? RegistrarClima(Clima clima);

        /**
         * Actualizaciones
         */

        /**
         * Eliminaciones
         */
        int EliminarClima(int id);
    }
}
