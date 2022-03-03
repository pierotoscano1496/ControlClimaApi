namespace ControlClimaApi.Models
{
    public class Ubicacion
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public double latitud { get; set; }
        public double longitud { get; set; }
    }
}
