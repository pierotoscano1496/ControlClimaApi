using MySql.Data.MySqlClient;

namespace ControlClimaApi.Models.DBContext.Interfaces
{
    public interface IControlClimaContext
    {
        MySqlConnection GetConnection();
    }
}
