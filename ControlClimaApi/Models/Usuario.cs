namespace ControlClimaApi.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Correo { get; set; }
        public string? Contrasena { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public List<Clima> Climas { get; set; }
    }
}
