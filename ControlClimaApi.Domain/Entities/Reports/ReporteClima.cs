using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlClimaApi.Domain.Entities.Reports
{
    public class ReporteClima
    {
        public string Medida { get; set; }
        public double Promedio { get; set; }
        public double Mediana { get; set; }
        public double Moda { get; set; }
        public string UnidadMedida { get; set; }
        public double Max { get; set; }
        public double Min { get; set; }
        public DateTime FechaMax { get; set; }
        public DateTime FechaMin { get; set; }
    }
}
