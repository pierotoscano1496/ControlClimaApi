using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlClimaApi.Domain.Entities
{
    public class Clima
    {
        public int Id { get; set; }
        public DateTime FechaRegistro { get; set; }
        public Usuario Usuario { get; set; }
        public Ubicacion Ubicacion { get; set; }
        public double Temperatura { get; set; }
        public double IntensidadLuz { get; set; }
        public double IntensidadViento { get; set; }
        public double? IntensidadCaudal { get; set; }
    }
}
