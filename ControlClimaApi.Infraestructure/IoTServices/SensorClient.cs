using ControlClimaApi.Domain.Abstractions.IoTServices;
using ControlClimaApi.Domain.Entities;
using ControlClimaApi.Domain.Entities.IoT;
using ControlClimaApi.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ControlClimaApi.Infraestructure.IoTServices
{
    public class SensorClient : BaseIoTService, ISensorClient
    {
        public SensorClient(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
        {
        }

        public async Task<Clima?> ObtenerDatosClima(int idUbicacion)
        {
            var client = GetPostClient();
            var response = await client.GetAsync($"datos-clima?id_ubicacion={idUbicacion}");
            if (response.IsSuccessStatusCode)
            {
                var datosClimaString = await response.Content.ReadAsStringAsync();
                Clima clima = JsonSerializer.Deserialize<Clima>(datosClimaString);

                return clima;
            }
            else
            {
                throw new BadGatewayException($"Error del servidor IoT. Status code: {response.StatusCode}");
            }
        }
    }
}
