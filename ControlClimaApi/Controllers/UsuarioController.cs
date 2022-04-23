using ControlClimaApi.Domain.Abstractions.Services;
using ControlClimaApi.Domain.Entities;
using ControlClimaApi.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace ControlClimaApi.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class UsuarioController : Controller
    {
        private readonly ILogger _logger;
        private readonly IUsuarioService _service;

        public UsuarioController(ILoggerFactory loggerFactory, IUsuarioService service)
        {
            _logger = loggerFactory.CreateLogger<UsuarioController>();
            _service = service;
        }

        [HttpPost("[action]")]
        public IActionResult Login([FromBody] Usuario credentials)
        {
            try
            {
                Usuario usuarioLogged = _service.Login(credentials);
                return Json(usuarioLogged);
            }
            catch (ForbiddenException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        public IActionResult ObtenerUsuarios()
        {
            try
            {
                List<Usuario> usuarios = _service.ObtenerUsuarios();
                return Json(usuarios);
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
        public IActionResult ObtenerUsuario(int id)
        {
            try
            {
                Usuario usuario = _service.ObtenerUsuario(id);
                return Json(usuario);
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
        public IActionResult RegistrarUsuario([FromBody] Usuario usuario)
        {
            try
            {
                Usuario usuarioRegistrado = _service.RegistrarUsuario(usuario);
                return Created("Usuario creado", usuarioRegistrado);
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
    }
}
