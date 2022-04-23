using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlClimaApi.Infraestructure.Repositories
{
    public class DataBaseConnection
    {
        protected MySqlConnection GetConnection()
        {
            return new MySqlConnection("Server=localhost;Port=3306;Database=control_clima;Uid=piero;Pwd=piero1496;");
        }
    }
}
