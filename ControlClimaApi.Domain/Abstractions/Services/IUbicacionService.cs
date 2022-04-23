using ControlClimaApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlClimaApi.Domain.Abstractions.Services
{
    public interface IUbicacionService
    {
        /**
         * Consultas
         */
        List<Ubicacion> ObtenerUbicaciones();
        Ubicacion ObtenerUbicacion(int id);
    }
}
