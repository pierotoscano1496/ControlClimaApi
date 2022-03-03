using ControlClimaApi.Models;
using ControlClimaApi.Models.DBContext.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ControlClimaApi.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class ClimaController : Controller
    {
        private readonly ILogger _logger;
        private readonly IClimaContext _context;

        public ClimaController(ILoggerFactory loggerFactory, IClimaContext context)
        {
            _logger = loggerFactory.CreateLogger<ClimaController>();
            _context = context;
        }

        [HttpGet("usuario/{idUsuario}")]
        public IActionResult ObtenerClimasPorUsuario(int idUsuario)
        {
            try
            {
                if (idUsuario > 0)
                {
                    List<Clima> climas = _context.ObtenerClimasPorUsuario(idUsuario);
                    if (climas.Count > 0)
                    {
                        return Json(climas);
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                else
                {
                    return BadRequest();
                }
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
                if (idUbicacion > 0)
                {
                    List<Clima> climas = _context.ObtenerClimasPorUbicacion(idUbicacion);
                    if (climas.Count > 0)
                    {
                        return Json(climas);
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                else
                {
                    return BadRequest();
                }
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
                List<Clima> climas = _context.ObtenerClimasDetalle(null);
                if (climas.Count > 0)
                {
                    return Json(climas);
                }
                else
                {
                    return NotFound();
                }
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
                if (id > 0)
                {
                    List<Clima> climas = _context.ObtenerClimasDetalle(id);
                    Clima? clima = climas.FirstOrDefault();
                    if (clima != null)
                    {
                        return Json(clima);
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        public IActionResult RegistrarClima(Clima clima)
        {
            try
            {
                Clima climaRegistered = _context.RegistrarClima(clima);
                if (climaRegistered != null)
                {
                    return Created("Clima creado", climaRegistered);
                }
                else
                {
                    return Accepted();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
