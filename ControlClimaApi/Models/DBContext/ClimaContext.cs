using ControlClimaApi.Models.DBContext.Interfaces;
using MySql.Data.MySqlClient;
using System.Data;

namespace ControlClimaApi.Models.DBContext
{
    public class ClimaContext : IClimaContext
    {
        private readonly IControlClimaContext _context;

        public ClimaContext(IControlClimaContext context)
        {
            _context = context;
        }

        /**
         * Consultas
         */
        public List<Clima> ObtenerClimasPorUsuario(int idUsuario)
        {
            try
            {
                List<Clima> climas = new List<Clima>();
                using (MySqlConnection connection = _context.GetConnection())
                {
                    connection.Open();
                    MySqlCommand command = new MySqlCommand("obtener_climas_por_usuario", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@idUsuario", idUsuario);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Clima clima = new Clima()
                            {
                                Id = reader.GetInt32("id"),
                                Codigo = reader.GetString("codigo"),
                                Latitud = reader.GetDouble("latitud"),
                                Longitud = reader.GetDouble("longitud"),
                                FechaRegistro = reader.GetDateTime("fecha_registro"),
                                Usuario = new Usuario()
                                {
                                    Id = reader.GetInt32("id_usuario")
                                },
                                Ubicacion = new Ubicacion()
                                {
                                    Id = reader.GetInt32("id_ubicacion")
                                },
                                IntensidadLuz = reader.GetDouble("intensidad_luz"),
                                VelocidadViento = reader.GetDouble("velocidad_viento"),
                                VelocidadCaudal = !reader.IsDBNull("velocidad_caudal") ? reader.GetDouble("velocidad_caudal") : null
                            };

                            climas.Add(clima);
                        }
                    }

                    return climas;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Clima> ObtenerClimasDetalle(int? id)
        {
            try
            {
                List<Clima> climas = new List<Clima>();
                using (MySqlConnection connection = _context.GetConnection())
                {
                    connection.Open();
                    MySqlCommand command = new MySqlCommand("obtener_climas_detalle", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@idClima", id);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Clima clima = new Clima()
                            {
                                Id = reader.GetInt32("id"),
                                Codigo = reader.GetString("codigo"),
                                Latitud = reader.GetDouble("latitud"),
                                Longitud = reader.GetDouble("longitud"),
                                FechaRegistro = reader.GetDateTime("fecha_registro"),
                                IntensidadLuz = reader.GetDouble("intensidad_luz"),
                                VelocidadViento = reader.GetDouble("velocidad_viento"),
                                VelocidadCaudal = reader["velocidad_caudal"] != null ? reader.GetDouble("velocidad_caudal") : null,
                                Usuario = new Usuario()
                                {
                                    Id = reader.GetInt32("id_usuario"),
                                    Codigo = reader.GetString("codigo_usuario"),
                                    Nombres = reader.GetString("nombres_usuario"),
                                    Apellidos = reader.GetString("apellidos_usuario"),
                                    Correo = reader.GetString("correo_usuario"),
                                    FechaNacimiento = reader.GetDateTime("fecha_nacimiento_usuario")
                                },
                                Ubicacion = new Ubicacion()
                                {
                                    Id = reader.GetInt32("id_ubicacion"),
                                    Nombre = reader["nombre_ubicacion"] != null ? reader.GetString("nombre_ubicacion") : null,
                                    latitud = reader.GetDouble("latitud_ubicacion"),
                                    longitud = reader.GetDouble("longitud_ubicacion")
                                }
                            };

                            climas.Add(clima);
                        }
                    }
                }

                return climas;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Clima> ObtenerClimasPorUbicacion(int idUbicacion)
        {
            try
            {
                List<Clima> climas = new List<Clima>();
                using (MySqlConnection connection = _context.GetConnection())
                {
                    connection.Open();
                    MySqlCommand command = new MySqlCommand("obtener_climas_por_ubicacion", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@idUbicacion", idUbicacion);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Clima clima = new Clima()
                            {
                                Id = reader.GetInt32("id"),
                                Codigo = reader.GetString("codigo"),
                                Latitud = reader.GetDouble("latitud"),
                                Longitud = reader.GetDouble("longitud"),
                                FechaRegistro = reader.GetDateTime("fecha_registro"),
                                Usuario = new Usuario()
                                {
                                    Id = reader.GetInt32("id_usuario")
                                },
                                Ubicacion = new Ubicacion()
                                {
                                    Id = reader.GetInt32("id_ubicacion")
                                },
                                IntensidadLuz = reader.GetDouble("intensidad_luz"),
                                VelocidadViento = reader.GetDouble("velocidad_viento"),
                                VelocidadCaudal = !reader.IsDBNull("velocidad_caudal") ? reader.GetDouble("velocidad_caudal") : null
                            };

                            climas.Add(clima);
                        }
                    }

                    return climas;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /**
         * Registros
         */
        public Clima? RegistrarClima(Clima clima)
        {
            try
            {
                Clima? climaRegistered = null;
                using (MySqlConnection connection = _context.GetConnection())
                {
                    connection.Open();
                    MySqlCommand command = new MySqlCommand("registrar_clima", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@codigoClima", clima.Codigo);
                    command.Parameters.AddWithValue("@latitudClima", clima.Latitud);
                    command.Parameters.AddWithValue("@longitudClima", clima.Longitud);
                    command.Parameters.AddWithValue("@idUsuario", clima.Usuario.Id);
                    command.Parameters.AddWithValue("@idUbicacion", clima.Ubicacion.Id);
                    command.Parameters.AddWithValue("@intensidadLuz", clima.IntensidadLuz);
                    command.Parameters.AddWithValue("@velocidadViento", clima.VelocidadViento);
                    command.Parameters.AddWithValue("@velocidadCaudal", clima.VelocidadCaudal);
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            climaRegistered = new Clima()
                            {
                                Id = reader.GetInt32(0),
                                Codigo = reader.GetString("codigo"),
                                Latitud = reader.GetDouble("latitud"),
                                Longitud = reader.GetDouble("longitud"),
                                Usuario = new Usuario()
                                {
                                    Id = reader.GetInt32("id_usuario")
                                },
                                Ubicacion = new Ubicacion()
                                {
                                    Id = reader.GetInt32("id_ubicacion")
                                },
                                IntensidadLuz = reader.GetDouble("intensidad_luz"),
                                VelocidadViento = reader.GetDouble("velocidad_viento"),
                                VelocidadCaudal = reader["velocidad_caudal"] != null ? reader.GetDouble("velocidad_caudal") : null
                            };
                        }
                    }
                }

                return climaRegistered;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /**
         * Eliminaciones
         */
        public int EliminarClima(int id)
        {
            try
            {
                int idClimaDeleted = 0;
                using (MySqlConnection connection = _context.GetConnection())
                {
                    connection.Open();
                    MySqlCommand command = new MySqlCommand("eliminar_clima", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@idClima", id);
                    idClimaDeleted = (Int32)command.ExecuteScalar();

                    return idClimaDeleted;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
