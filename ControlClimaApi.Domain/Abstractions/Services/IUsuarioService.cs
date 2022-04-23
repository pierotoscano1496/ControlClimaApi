using ControlClimaApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlClimaApi.Domain.Abstractions.Services
{
    public interface IUsuarioService
    {
        /**
         * Consultas
         */
        List<Usuario> ObtenerUsuarios();
        Usuario ObtenerUsuario(int id);
        Usuario Login(Usuario usuario);

        /**
         * Registros
         */
        Usuario RegistrarUsuario(Usuario usuario);
    }
}
