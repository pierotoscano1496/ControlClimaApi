using ControlClimaApi.Models.DBContext.Interfaces;
using MySql.Data.MySqlClient;
using System.Data;

namespace ControlClimaApi.Models.DBContext
{
    public class UsuarioContext : IUsuarioContext
    {
        private readonly IControlClimaContext _context;

        public UsuarioContext(IControlClimaContext context)
        {
            _context = context;
        }

        public List<Usuario> ObtenerUsuarios(int? id)
        {
            try
            {
                List<Usuario> usuarios = new List<Usuario>();
                using (MySqlConnection connection = _context.GetConnection())
                {
                    connection.Open();
                    MySqlCommand command = new MySqlCommand("obtener_usuarios", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@idUsuario", id);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Usuario usuario = new Usuario()
                            {
                                Id = reader.GetInt32("id"),
                                Codigo = reader.GetString("codigo"),
                                Nombres = reader.GetString("nombres"),
                                Apellidos = reader.GetString("apellidos"),
                                Correo = reader.GetString("correo"),
                                Contrasena = null,
                                FechaNacimiento = reader.GetDateTime("fecha_nacimiento")
                            };

                            usuarios.Add(usuario);
                        }
                    }
                }

                return usuarios;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Usuario? Login(UsuarioCredenciales credentials)
        {
            try
            {
                Usuario? usuarioLogged = null;
                using (MySqlConnection connection = _context.GetConnection())
                {
                    connection.Open();
                    MySqlCommand command = new MySqlCommand("login_usuario", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@cred", credentials.Credential);
                    command.Parameters.AddWithValue("@pass", credentials.Password);
                    command.Parameters.AddWithValue("@keyStr", credentials.Key);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            usuarioLogged = new Usuario()
                            {
                                Id = reader.GetInt32("id"),
                                Codigo = reader.GetString("codigo"),
                                Nombres = reader.GetString("nombres"),
                                Apellidos = reader.GetString("apellidos"),
                                Correo = reader.GetString("correo"),
                                Contrasena = null,
                                FechaNacimiento = reader.GetDateTime("fecha_nacimiento")
                            };
                        }
                    }
                }

                return usuarioLogged;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Usuario? RegistrarUsuario(UsuarioParametros usuarioParametros)
        {
            try
            {
                Usuario? usuarioRegistered = null;
                using (MySqlConnection connection = _context.GetConnection())
                {
                    connection.Open();
                    MySqlCommand command = new MySqlCommand("registrar_usuario", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@codigoUsuario", usuarioParametros.Usuario.Codigo);
                    command.Parameters.AddWithValue("@nombresUsuario", usuarioParametros.Usuario.Nombres);
                    command.Parameters.AddWithValue("@apellidosUsuario", usuarioParametros.Usuario.Apellidos);
                    command.Parameters.AddWithValue("@correoUsuario", usuarioParametros.Usuario.Correo);
                    command.Parameters.AddWithValue("@contrasenaUsuario", usuarioParametros.Usuario.Contrasena);
                    command.Parameters.AddWithValue("@keyStr", usuarioParametros.Key);
                    command.Parameters.AddWithValue("@fechaNacimiento", usuarioParametros.Usuario.FechaNacimiento);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            usuarioRegistered = new Usuario()
                            {
                                Id = reader.GetInt32("id"),
                                Codigo = reader.GetString("codigo"),
                                Nombres = reader.GetString("nombres"),
                                Apellidos = reader.GetString("apellidos"),
                                Correo = reader.GetString("correo"),
                                Contrasena = null,
                                FechaNacimiento = reader.GetDateTime("fecha_nacimiento")
                            };
                        }
                    }
                }

                return usuarioRegistered;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
