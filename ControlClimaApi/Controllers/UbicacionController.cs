using ControlClimaApi.Domain.Abstractions.Services;
using ControlClimaApi.Domain.Entities;
using ControlClimaApi.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace ControlClimaApi.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class UbicacionController : Controller
    {
        private readonly ILogger _logger;
        private readonly IUbicacionService _service;

        public UbicacionController(ILoggerFactory loggerFactory, IUbicacionService service)
        {
            _logger = loggerFactory.CreateLogger<UbicacionController>();
            _service = service;
        }

        [HttpGet]
        public IActionResult ObtenerUbicaciones()
        {
            try
            {
                List<Ubicacion> ubicaciones = _service.ObtenerUbicaciones();
                return Json(ubicaciones);
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

        [HttpGet("{id}")]
        public IActionResult ObtenerUbicacion(int id)
        {
            try
            {
                Ubicacion ubicacion = _service.ObtenerUbicacion(id);
                return Json(ubicacion);
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
    }
}
