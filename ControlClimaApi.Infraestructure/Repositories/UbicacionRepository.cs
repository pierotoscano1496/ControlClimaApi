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
    public class UbicacionRepository : DataBaseConnection, IUbicacionRepository
    {
        public List<Ubicacion> ObtenerUbicaciones(int? id)
        {
            List<Ubicacion> ubicaciones = new List<Ubicacion>();
            using (MySqlConnection connection = GetConnection())
            {
                connection.Open();
                MySqlCommand command = new MySqlCommand("obtener_ubicaciones", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@idUbicacion", id);

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Ubicacion ubicacion = new Ubicacion()
                        {
                            Id = reader.GetInt32("id"),
                            Latitud = reader.GetDouble("latitud"),
                            Longitud = reader.GetDouble("longitud"),
                            Nombre = !reader.IsDBNull("nombre") ? reader.GetString("nombre") : null
                        };

                        ubicaciones.Add(ubicacion);
                    }
                }
            }

            return ubicaciones;
        }
    }
}
