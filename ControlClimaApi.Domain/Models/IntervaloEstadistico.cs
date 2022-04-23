using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlClimaApi.Domain.Models
{
    public class IntervaloEstadistico
    {
        public int Orden { get; set; }
        public double MinValor { get; set; }
        public double MaxValor { get; set; }
        public int Frecuencia { get; set; }
        public int FrecuenciaAbsoluta { get; set; }
    }
}
