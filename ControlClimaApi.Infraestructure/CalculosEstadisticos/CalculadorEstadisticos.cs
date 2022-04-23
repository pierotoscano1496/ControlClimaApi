using ControlClimaApi.Domain.Abstractions.CalculosEstadisticos;
using ControlClimaApi.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlClimaApi.Infraestructure.CalculosEstadisticos
{
    public class CalculadorEstadisticos : ICalculadorEstadisticos
    {
        // Cálculos generales
        private List<IntervaloEstadistico> ArmarIntervalos(IEnumerable<double> data)
        {
            // Estadísticos
            int cantidad = data.Count();
            int k = (int)Math.Round(1 + 3.3 * Math.Log10(cantidad), MidpointRounding.AwayFromZero);

            IOrderedEnumerable<double> dataOrdenada = data.OrderBy(x => x);
            double max = data.Max();
            double min = data.Min();
            double rango = max - min;
            int amplitud = (int)Math.Round(rango / k, MidpointRounding.AwayFromZero);

            // Intervalos
            List<IntervaloEstadistico> intervalos = new List<IntervaloEstadistico>();
            int numOrden = 1;
            double limInferior = min;

            do
            {
                IntervaloEstadistico? intervaloAnterior = intervalos.Find(i => i.Orden == numOrden - 1);
                double limSuperior = limInferior + amplitud > max ? max : limInferior + amplitud;
                int frecuencia = data.Where(d => d >= limInferior && d < limSuperior).Count();

                IntervaloEstadistico intervalo = new IntervaloEstadistico()
                {
                    Orden = numOrden,
                    Frecuencia = frecuencia,
                    FrecuenciaAbsoluta = intervaloAnterior != null ? frecuencia + intervaloAnterior.Frecuencia : frecuencia,
                    MinValor = limInferior,
                    MaxValor = limSuperior
                };

                intervalos.Add(intervalo);
                numOrden++;
                limInferior += amplitud;
            } while (limInferior <= max);

            return intervalos;
        }

        // Mediana
        private double MedianaAgrupados(IEnumerable<double> data)
        {
            // Estadísticos
            int cantidad = data.Count();
            int posicion = cantidad % 2 == 0 ? cantidad / 2 : (cantidad + 1) / 2;

            List<IntervaloEstadistico> intervalos = ArmarIntervalos(data);

            // Ubicación de intervalo de la mediana
            IntervaloEstadistico intervaloMediana = intervalos.Find(i => posicion <= i.FrecuenciaAbsoluta);
            int frecuenciaAbsolutaAnterior = intervaloMediana.Orden > 1 ? intervalos.Find(i => i.Orden == intervaloMediana.Orden - 1).FrecuenciaAbsoluta : 0;

            double mediana = intervaloMediana.MinValor * (cantidad / 2 - frecuenciaAbsolutaAnterior) * (intervaloMediana.MaxValor - intervaloMediana.MinValor) / intervaloMediana.Frecuencia;

            return mediana;
        }

        private double MedianaNoAgrupados(IEnumerable<double> data)
        {
            int cantidad = data.Count();
            List<double> dataOrdenada = data.OrderBy(x => x).ToList();
            double? mediana = null;

            if (cantidad % 2 == 0)
            {
                int posicion1 = cantidad / 2;
                int posicion2 = posicion1 + 1;

                int index = 1;
                double? dato1 = null;
                double? dato2 = null;
                dataOrdenada.ForEach(d =>
                {
                    if (index == posicion1)
                    {
                        dato1 = d;
                    }
                    if (index == posicion2)
                    {
                        dato2 = d;
                        return;
                    }

                    index++;
                });

                mediana = (dato1!.Value + dato2!.Value) / 2;
            }
            else
            {
                int posicion = (cantidad + 1) / 2;
                int index = 1;
                dataOrdenada.ForEach(d =>
                {
                    if (posicion == index)
                    {
                        mediana = d;
                        return;
                    }

                    index++;
                });
            }

            return mediana!.Value;
        }

        public double Mediana(IEnumerable<double> data)
        {
            double max = data.Max();
            double min = data.Min();

            if (max - min > 10)
            {
                return MedianaAgrupados(data);
            }
            else
            {
                return MedianaNoAgrupados(data);
            }
        }

        public double? Mediana(IEnumerable<double?> data)
        {
            if (data.All(d => d != null))
            {
                return Mediana(data);
            }
            else
            {
                return null;
            }
        }

        // Promedio
        private double PromedioAgrupados(IEnumerable<double> data)
        {
            int cantidad = data.Count();
            List<IntervaloEstadistico> intervalos = ArmarIntervalos(data);
            double sumaMarcaFrecuencia = 0;
            intervalos.ForEach(i =>
            {
                double marca = (i.MaxValor - i.MinValor) / 2;
                sumaMarcaFrecuencia += i.Frecuencia * marca;
            });

            return sumaMarcaFrecuencia / cantidad;
        }

        private double PromedioNoAgrupados(IEnumerable<double> data)
        {
            return data.Average();
        }

        public double Promedio(IEnumerable<double> data)
        {
            double max = data.Max();
            double min = data.Min();

            if (max - min > 10)
            {
                return PromedioAgrupados(data);
            }
            else
            {
                return PromedioNoAgrupados(data);
            }
        }

        public double? Promedio(IEnumerable<double?> data)
        {
            if (data.All(d => d != null))
            {
                return Promedio(data);
            }
            else
            {
                return null;
            }
        }

        // Moda
        private double ModaAgrupados(IEnumerable<double> data)
        {
            List<IntervaloEstadistico> intervalos = ArmarIntervalos(data);
            double frecuenciaModa = intervalos.Max(i => i.Frecuencia);
            IntervaloEstadistico intervaloModa = intervalos.Find(i => i.Frecuencia == frecuenciaModa);

            IntervaloEstadistico intervaloAnterior = intervalos.Find(i => i.Orden == intervaloModa!.Orden - 1);
            double frecuenciaAnterior = intervaloAnterior != null ? intervaloAnterior.Frecuencia : 0;

            IntervaloEstadistico intervalorSiguiente = intervalos.Find(i => i.Orden == intervaloModa!.Orden + 1);
            double frecuenciaSiguiente = intervalorSiguiente != null ? intervalorSiguiente.Frecuencia : 0;

            int amplitudIntervaloModa = (int)Math.Round(intervaloModa.MaxValor - intervaloModa.MinValor, MidpointRounding.AwayFromZero);
            double moda = intervaloModa.MinValor + ((frecuenciaModa - frecuenciaAnterior) / ((frecuenciaModa - frecuenciaAnterior) + (frecuenciaModa - frecuenciaSiguiente))) * amplitudIntervaloModa;

            return moda;
        }

        private double ModaNoAgrupados(IEnumerable<double> data)
        {
            double moda = data.GroupBy(d => d).OrderByDescending(d => d.Count()).Select(d => d.Key).First();
            return moda;
        }

        public double Moda(IEnumerable<double> data)
        {
            double max = data.Max();
            double min = data.Min();

            if (max - min > 10)
            {
                return ModaAgrupados(data);
            }
            else
            {
                return ModaNoAgrupados(data);
            }
        }

        public double? Moda(IEnumerable<double?> data)
        {
            if (data.All(d => d != null))
            {
                return Moda(data);
            }
            else
            {
                return null;
            }
        }

    }
}
