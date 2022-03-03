using ControlClimaApi.Models.DBContext.Interfaces;
using MySql.Data.MySqlClient;

namespace ControlClimaApi.Models
{
    public class ControlClimaContext : IControlClimaContext
    {
        private readonly IConfiguration _configuration;

        public ControlClimaContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public MySqlConnection GetConnection()
        {
            return new MySqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        }
    }
}
