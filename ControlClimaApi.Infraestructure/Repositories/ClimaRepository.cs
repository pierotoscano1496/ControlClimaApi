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
    public class ClimaRepository : DataBaseConnection, IClimaRepository
    {
        public List<Clima> ObtenerClimasDetalle(int? id)
        {
            List<Clima> climas = new List<Clima>();
            using (MySqlConnection connection = GetConnection())
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
                            FechaRegistro = reader.GetDateTime("fecha_registro"),
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
                                Nombre = !reader.IsDBNull("nombre_ubicacion") ? reader.GetString("nombre_ubicacion") : null,
                                Latitud = reader.GetDouble("latitud_ubicacion"),
                                Longitud = reader.GetDouble("longitud_ubicacion")
                            },
                            Temperatura = reader.GetDouble("temperatura"),
                            IntensidadLuz = reader.GetDouble("intensidad_luz"),
                            IntensidadViento = reader.GetDouble("intensidad_viento"),
                            IntensidadCaudal = !reader.IsDBNull("intensidad_caudal") ? reader.GetDouble("intensidad_caudal") : null
                        };

                        climas.Add(clima);
                    }
                }
            }

            return climas;
        }

        public List<Clima> ObtenerClimasFormulario(int idUsuario, DateTime fechaInicio, DateTime fechaFin, int? idUbicacion = null)
        {
            List<Clima> climas = new List<Clima>();
            using (MySqlConnection connection = GetConnection())
            {
                connection.Open();
                MySqlCommand command = new MySqlCommand("obtener_climas_formulario", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@idUsuario", idUsuario);
                command.Parameters.AddWithValue("@fechaInicio", fechaInicio);
                command.Parameters.AddWithValue("@fechaFin", fechaFin);
                command.Parameters.AddWithValue("@idUbicacion", idUbicacion);

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Clima clima = new Clima()
                        {
                            Id = reader.GetInt32("id"),
                            FechaRegistro = reader.GetDateTime("fecha_registro"),
                            Usuario = new Usuario()
                            {
                                Id = reader.GetInt32("id_usuario")
                            },
                            Ubicacion = new Ubicacion()
                            {
                                Id = reader.GetInt32("id_ubicacion")
                            },
                            Temperatura = reader.GetDouble("temperatura"),
                            IntensidadLuz = reader.GetDouble("intensidad_luz"),
                            IntensidadViento = reader.GetDouble("intensidad_viento"),
                            IntensidadCaudal = !reader.IsDBNull("intensidad_caudal") ? reader.GetDouble("intensidad_caudal") : null
                        };

                        climas.Add(clima);
                    }
                }

                return climas;
            }
        }

        public List<Clima> ObtenerClimasPorUbicacion(int idUbicacion)
        {
            List<Clima> climas = new List<Clima>();
            using (MySqlConnection connection = GetConnection())
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
                            FechaRegistro = reader.GetDateTime("fecha_registro"),
                            Usuario = new Usuario()
                            {
                                Id = reader.GetInt32("id_usuario")
                            },
                            Ubicacion = new Ubicacion()
                            {
                                Id = reader.GetInt32("id_ubicacion")
                            },
                            Temperatura = reader.GetDouble("temperatura"),
                            IntensidadLuz = reader.GetDouble("intensidad_luz"),
                            IntensidadViento = reader.GetDouble("intensidad_viento"),
                            IntensidadCaudal = !reader.IsDBNull("intensidad_caudal") ? reader.GetDouble("intensidad_caudal") : null
                        };

                        climas.Add(clima);
                    }
                }

                return climas;
            }
        }

        public List<Clima> ObtenerClimasPorUsuario(int idUsuario)
        {
            List<Clima> climas = new List<Clima>();
            using (MySqlConnection connection = GetConnection())
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
                            FechaRegistro = reader.GetDateTime("fecha_registro"),
                            Usuario = new Usuario()
                            {
                                Id = reader.GetInt32("id_usuario")
                            },
                            Ubicacion = new Ubicacion()
                            {
                                Id = reader.GetInt32("id_ubicacion")
                            },
                            Temperatura = reader.GetDouble("temperatura"),
                            IntensidadLuz = reader.GetDouble("intensidad_luz"),
                            IntensidadViento = reader.GetDouble("intensidad_viento"),
                            IntensidadCaudal = !reader.IsDBNull("intensidad_caudal") ? reader.GetDouble("intensidad_caudal") : null
                        };

                        climas.Add(clima);
                    }
                }

                return climas;
            }
        }

        public List<Clima> ObtenerFuenteClimaReporte(int idUbicacion, DateTime fechaInicio, DateTime fechaFin)
        {
            List<Clima> climas = new List<Clima>();
            using (MySqlConnection connection = GetConnection())
            {
                connection.Open();
                MySqlCommand command = new MySqlCommand("fuente_reporte_clima", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("idUbicacion", idUbicacion);
                command.Parameters.AddWithValue("fechaInicio", fechaInicio);
                command.Parameters.AddWithValue("fechaFin", fechaFin);

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Clima clima = new Clima();
                        clima.Id = reader.GetInt32("id");
                        clima.FechaRegistro = reader.GetDateTime("fecha_registro");
                        clima.Usuario = new Usuario()
                        {
                            Id = reader.GetInt32("id_usuario")
                        };
                        clima.Ubicacion = new Ubicacion()
                        {
                            Id = reader.GetInt32("id_ubicacion")
                        };
                        clima.Temperatura = reader.GetDouble("temperatura");
                        clima.IntensidadLuz = reader.GetDouble("intensidad_luz");
                        clima.IntensidadViento = reader.GetDouble("intensidad_viento");
                        clima.IntensidadCaudal = reader.IsDBNull("intensidad_caudal") ? reader.GetDouble("intensidad_caudal") : null;

                        climas.Add(clima);
                    }
                }
            }

            return climas;
        }

        public Clima? RegistrarClima(Clima clima)
        {
            Clima? climaRegistered = null;
            using (MySqlConnection connection = GetConnection())
            {
                connection.Open();
                MySqlCommand command = new MySqlCommand("registrar_clima", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@idUsuario", clima.Usuario.Id);
                command.Parameters.AddWithValue("@idUbicacion", clima.Ubicacion.Id);
                command.Parameters.AddWithValue("@temperatura", clima.Temperatura);
                command.Parameters.AddWithValue("@intensidadLuz", clima.IntensidadLuz);
                command.Parameters.AddWithValue("@intensidadViento", clima.IntensidadViento);
                command.Parameters.AddWithValue("@intensidadCaudal", clima.IntensidadCaudal);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        climaRegistered = new Clima()
                        {
                            Id = reader.GetInt32(0),
                            FechaRegistro = reader.GetDateTime("fecha_registro"),
                            Usuario = new Usuario()
                            {
                                Id = reader.GetInt32("id_usuario")
                            },
                            Ubicacion = new Ubicacion()
                            {
                                Id = reader.GetInt32("id_ubicacion")
                            },
                            Temperatura = reader.GetDouble("temperatura"),
                            IntensidadLuz = reader.GetDouble("intensidad_luz"),
                            IntensidadViento = reader.GetDouble("intensidad_viento"),
                            IntensidadCaudal = !reader.IsDBNull("intensidad_caudal") ? reader.GetDouble("intensidad_caudal") : null
                        };
                    }
                }
            }

            return climaRegistered;
        }

        public int EliminarClima(int id)
        {
            int idClimaDeleted = 0;
            using (MySqlConnection connection = GetConnection())
            {
                connection.Open();
                MySqlCommand command = new MySqlCommand("eliminar_clima", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@idClima", id);
                idClimaDeleted = (Int32)command.ExecuteScalar();
            }

            return idClimaDeleted;
        }
    }
}
