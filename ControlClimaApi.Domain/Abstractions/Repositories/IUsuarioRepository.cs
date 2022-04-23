using ControlClimaApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlClimaApi.Domain.Abstractions.Repositories
{
    public interface IUsuarioRepository
    {
        /**
         * Consultas
         */
        List<Usuario> ObtenerUsuarios(int? id);
        Usuario? Login(Usuario usuario);

        /**
         * Registros
         */
        Usuario? RegistrarUsuario(Usuario usuario);
    }
}
