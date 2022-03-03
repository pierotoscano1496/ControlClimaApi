using ControlClimaApi.Models;
using ControlClimaApi.Models.DBContext;
using ControlClimaApi.Models.DBContext.Interfaces;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System.Data;

namespace ControlClimaApi.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class UsuarioController : Controller
    {
        private readonly ILogger _logger;
        private readonly IUsuarioContext _context;

        public UsuarioController(ILoggerFactory loggerFactory, IUsuarioContext context)
        {
            _logger = loggerFactory.CreateLogger<UsuarioController>();
            _context = context;
        }

        [HttpPost("[action]")]
        public IActionResult Login([FromBody] UsuarioCredenciales credentials)
        {
            try
            {
                Usuario? usuarioLogged = _context.Login(credentials);
                if (usuarioLogged != null)
                {
                    return Json(usuarioLogged);
                }
                else
                {
                    return Unauthorized();
                }
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
                List<Usuario> usuarios = _context.ObtenerUsuarios(null);
                if (usuarios.Count > 0)
                {
                    return Json(usuarios);
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

        [HttpGet("{id}")]
        public IActionResult ObtenerUsuario(int id)
        {
            try
            {
                List<Usuario> usuarios = _context.ObtenerUsuarios(id);
                Usuario? usuario = usuarios.FirstOrDefault();
                if (usuario != null)
                {
                    return Json(usuario);
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

        [HttpPost]
        public IActionResult RegistrarUsuario([FromBody] UsuarioParametros usuarioParametros)
        {
            try
            {
                Usuario usuarioRegistered = _context.RegistrarUsuario(usuarioParametros);
                if (usuarioRegistered != null)
                {
                    return Created("Usuario creado", usuarioRegistered);
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
