namespace ControlClimaApi.Models.DBContext.Interfaces
{
    public interface IClimaContext
    {
        /**
         * Consultas
         */
        List<Clima> ObtenerClimasPorUsuario(int idUsuario);
        List<Clima> ObtenerClimasDetalle(int? id);
        List<Clima> ObtenerClimasPorUbicacion(int idUbicacion);

        /**
         * Registros
         */
        Clima? RegistrarClima(Clima clima);

        /**
         * Actualizaciones
         */

        /**
         * Eliminaciones
         */
        int EliminarClima(int id);
    }
}
