using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlClimaApi.Domain.Entities.IoT
{
    public class ClimaDatosSensor
    {
        public double Temperatura { get; set; }
        public double IntensidadLuminosa { get; set; }
        public double IntensidadViento { get; set; }
        public double? IntensidadCaudal { get; set; }
    }
}
