namespace ControlClimaApi.Models.DBContext.Interfaces
{
    public interface IUsuarioContext
    {
        List<Usuario> ObtenerUsuarios(int? id);
        Usuario? Login(UsuarioCredenciales credentials);
        Usuario? RegistrarUsuario(UsuarioParametros usuario);
    }
}
