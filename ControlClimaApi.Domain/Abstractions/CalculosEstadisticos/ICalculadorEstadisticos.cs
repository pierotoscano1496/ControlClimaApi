using ControlClimaApi.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlClimaApi.Domain.Abstractions.CalculosEstadisticos
{
    public interface ICalculadorEstadisticos
    {
        double Mediana(IEnumerable<double> data);
        double? Mediana(IEnumerable<double?> data);
        double Promedio(IEnumerable<double> data);
        double? Promedio(IEnumerable<double?> data);
        double Moda(IEnumerable<double> data);
        double? Moda(IEnumerable<double?> data);
    }
}
