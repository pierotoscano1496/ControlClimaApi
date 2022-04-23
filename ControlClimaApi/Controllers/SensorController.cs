using ControlClimaApi.Domain.Abstractions.IoTServices;
using ControlClimaApi.Domain.Entities;
using ControlClimaApi.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace ControlClimaApi.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class SensorController : Controller
    {
        public ILogger _logger;
        public ISensorClient _sensorClient;

        public SensorController(ILoggerFactory loggerFactory, ISensorClient sensorClient)
        {
            _logger = loggerFactory.CreateLogger<SensorController>();
            _sensorClient = sensorClient;
        }

        [HttpGet("datos-clima/{idUbicacion}")]
        public async Task<IActionResult> datosClima(int idUbicacion)
        {
            try
            {
                Clima? clima = await _sensorClient.ObtenerDatosClima(idUbicacion);
                return Ok(clima);
            }
            catch (BadGatewayException ex)
            {
                return StatusCode(StatusCodes.Status502BadGateway);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
