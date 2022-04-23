using ControlClimaApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlClimaApi.Domain.Abstractions.IoTServices
{
    public interface ISensorClient
    {
        Task<Clima?> ObtenerDatosClima(int idUbicacion);
    }
}
