using ControlClimaApi.Domain.Abstractions.Repositories;
using ControlClimaApi.Domain.Entities;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlClimaApi.Infraestructure.Repositories
{
    public class UsuarioRepository : DataBaseConnection, IUsuarioRepository
    {
        public Usuario? Login(Usuario usuario)
        {
            Usuario? usuarioLogged = null;
            using (MySqlConnection connection = GetConnection())
            {
                string credential = usuario.Codigo != null ? usuario.Codigo : usuario.Correo;
                connection.Open();
                MySqlCommand command = new MySqlCommand("login_usuario", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@cred", credential);
                command.Parameters.AddWithValue("@pass", usuario.Contrasena);

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

        public List<Usuario> ObtenerUsuarios(int? id)
        {
            List<Usuario> usuarios = new List<Usuario>();
            using (MySqlConnection connection = GetConnection())
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

        public Usuario? RegistrarUsuario(Usuario usuario)
        {
            Usuario? usuarioRegistered = null;
            using (MySqlConnection connection = GetConnection())
            {
                connection.Open();
                MySqlCommand command = new MySqlCommand("registrar_usuario", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@codigo", usuario.Codigo);
                command.Parameters.AddWithValue("@nombres", usuario.Nombres);
                command.Parameters.AddWithValue("@apellidos", usuario.Apellidos);
                command.Parameters.AddWithValue("@correo", usuario.Correo);
                command.Parameters.AddWithValue("@contrasena", usuario.Contrasena);
                command.Parameters.AddWithValue("@fechaNacimiento", usuario.FechaNacimiento);

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
    }
}
