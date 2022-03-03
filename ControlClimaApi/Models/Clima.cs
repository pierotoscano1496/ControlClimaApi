namespace ControlClimaApi.Models
{
    public class Clima
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public double Latitud { get; set; }
        public double Longitud { get; set; }
        public DateTime FechaRegistro { get; set; }
        public Usuario Usuario { get; set; }
        public Ubicacion Ubicacion { get; set; }
        public double IntensidadLuz { get; set; }
        public double VelocidadViento { get; set; }
        public double? VelocidadCaudal { get; set; }
    }
}
