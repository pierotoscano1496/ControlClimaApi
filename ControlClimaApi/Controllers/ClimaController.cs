using ControlClimaApi.Domain.Abstractions.Services;
using ControlClimaApi.Domain.Entities;
using ControlClimaApi.Domain.Entities.Reports;
using ControlClimaApi.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace ControlClimaApi.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class ClimaController : Controller
    {
        private readonly ILogger _logger;
        private readonly IClimaService _service;

        public ClimaController(ILoggerFactory loggerFactory, IClimaService service)
        {
            _logger = loggerFactory.CreateLogger<ClimaController>();
            _service = service;
        }

        [HttpGet("usuario/{idUsuario}")]
        public IActionResult ObtenerClimasPorUsuario(int idUsuario)
        {
            try
            {
                List<Clima> climas = _service.ObtenerClimasPorUsuario(idUsuario);
                return Json(climas);
            }
            catch (ResourceNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (BadRequestException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("ubicacion/{idUbicacion}")]
        public IActionResult ObtenerClimasPorUbicacion(int idUbicacion)
        {
            try
            {
                List<Clima> climas = _service.ObtenerClimasPorUbicacion(idUbicacion);
                return Json(climas);
            }
            catch (ResourceNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (BadRequestException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("detalle")]
        public IActionResult ObtenerClimasDetalle()
        {
            try
            {
                List<Clima> climas = _service.ObtenerClimasDetalle();
                return Json(climas);
            }
            catch (ResourceNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("detalle/{id}")]
        public IActionResult ObtenerClimaDetalle(int id)
        {
            try
            {
                Clima climas = _service.ObtenerClimaDetalle(id);
                return Json(climas);
            }
            catch (ResourceNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (BadRequestException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("consulta/{idUsuario}/{fechaInicio}/{fechaFin}/{idUbicacion?}")]
        public IActionResult ObtenerClimasFormulario(int idUsuario, DateTime fechaInicio, DateTime fechaFin, int? idUbicacion = null)
        {
            try
            {
                List<Clima> climas = _service.ObtenerClimasFormulario(idUsuario, fechaInicio, fechaFin, idUbicacion);
                return Json(climas);

                if (climas.Count > 0)
                {
                    return Json(climas);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (ResourceNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (BadRequestException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("reporte/{idUbicacion}/{fechaInicio}/{fechaFin}")]
        public IActionResult Reporte(int idUbicacion, DateTime fechaInicio, DateTime fechaFin)
        {
            try
            {
                List<ReporteClima> reporteClimas = _service.ObtenerClimaReporte(idUbicacion, fechaInicio, fechaFin);
                return Ok(reporteClimas);
            }
            catch (ResourceNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (BadRequestException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        public IActionResult RegistrarClima([FromBody] Clima clima)
        {
            try
            {
                Clima? climaRegistrado = _service.RegistrarClima(clima);
                return Created("Clima creado", climaRegistrado);
            }
            catch (NotCreatedException ex)
            {
                return StatusCode(StatusCodes.Status502BadGateway, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult EliminarClima(int id)
        {
            try
            {
                int idClimaEliminado = _service.EliminarClima(id);
                return Ok(idClimaEliminado);
            }
            catch (ResourceNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
