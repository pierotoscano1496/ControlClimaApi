using ControlClimaApi.Domain.Abstractions.Repositories;
using ControlClimaApi.Domain.Abstractions.Services;
using ControlClimaApi.Domain.Entities;
using ControlClimaApi.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlClimaApi.Aplication.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _repository;

        public UsuarioService(IUsuarioRepository repository)
        {
            _repository = repository;
        }

        public Usuario Login(Usuario usuario)
        {
            Usuario? usuarioLogged = _repository.Login(usuario);
            if (usuarioLogged != null)
            {
                return usuarioLogged;
            }
            else
            {
                throw new ForbiddenException("Correo, código o clave incorrectos");
            }
        }

        public List<Usuario> ObtenerUsuarios()
        {
            List<Usuario> usuarios = _repository.ObtenerUsuarios(null);
            if (usuarios.Any())
            {
                return usuarios;
            }
            else
            {
                throw new ResourceNotFoundException("No existen usuarios");
            }
        }

        public Usuario ObtenerUsuario(int id)
        {
            if (id > 0)
            {
                List<Usuario> usuarios = _repository.ObtenerUsuarios(id);
                if (usuarios.Any())
                {
                    return usuarios.First();
                }
                else
                {
                    throw new ResourceNotFoundException("No existe el usuario buscado");
                }
            }
            else
            {
                throw new BadRequestException("Id de usuario no válido");
            }
        }

        public Usuario RegistrarUsuario(Usuario usuario)
        {
            Usuario? usuarioRegistrado = _repository.RegistrarUsuario(usuario);
            if (usuarioRegistrado != null)
            {
                return usuarioRegistrado;
            }
            else
            {
                throw new NotCreatedException("No se creó el usuario solicitado");
            }
        }
    }
}
