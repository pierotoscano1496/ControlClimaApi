using ControlClimaApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlClimaApi.Domain.Abstractions.Repositories
{
    public interface IUbicacionRepository
    {
        /**
         * Consultas
         */
        List<Ubicacion> ObtenerUbicaciones(int? id);
    }
}
