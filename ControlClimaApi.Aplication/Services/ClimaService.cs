using ControlClimaApi.Domain.Abstractions.CalculosEstadisticos;
using ControlClimaApi.Domain.Abstractions.IoTServices;
using ControlClimaApi.Domain.Abstractions.Repositories;
using ControlClimaApi.Domain.Abstractions.Services;
using ControlClimaApi.Domain.Entities;
using ControlClimaApi.Domain.Entities.Reports;
using ControlClimaApi.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlClimaApi.Aplication.Services
{
    public class ClimaService : IClimaService
    {
        private readonly IClimaRepository _repository;
        private readonly ICalculadorEstadisticos _calculadorEstadistico;
        private readonly ISensorClient _sensorClient;

        public ClimaService(IClimaRepository repository, ICalculadorEstadisticos calculadorEstadisticos, ISensorClient sensorClient)
        {
            _repository = repository;
            _calculadorEstadistico = calculadorEstadisticos;
            _sensorClient = sensorClient;
        }

        public List<ReporteClima> ObtenerClimaReporte(int idUbicacion, DateTime fechaInicio, DateTime fechaFin)
        {
            if (idUbicacion > 0)
            {
                List<Clima> climas = _repository.ObtenerFuenteClimaReporte(idUbicacion, fechaInicio, fechaFin);
                if (climas.Count() > 0)
                {
                    List<ReporteClima> reportesClima = new List<ReporteClima>();

                    bool existsIntensidadCaudal = climas.Where(c => c.IntensidadCaudal != null).Count() > 0;
                    // Valores máximos
                    double maxTemperatura = climas.Max(c => c.Temperatura);
                    double maxIntensidadLuz = climas.Max(c => c.IntensidadLuz);
                    double maxIntensidadViento = climas.Max(c => c.IntensidadViento);
                    double? maxIntensidadCaudal = climas.Max(c => c.IntensidadCaudal);

                    // Valores mínimos
                    double minTemperatura = climas.Min(c => c.Temperatura);
                    double minIntensidadLuz = climas.Min(c => c.IntensidadLuz);
                    double minIntensidadViento = climas.Min(c => c.IntensidadViento);
                    double? minIntensidadCaudal = climas.Min(c => c.IntensidadCaudal);

                    // Fechas de valores máximos
                    DateTime fechaMaxTemperatura = climas.Where(c => c.Temperatura == maxTemperatura).First().FechaRegistro;
                    DateTime fechaMaxIntensidadLuz = climas.Where(c => c.IntensidadLuz == maxIntensidadLuz).First().FechaRegistro;
                    DateTime fechaMaxIntensidadViento = climas.Where(c => c.IntensidadViento == maxIntensidadViento).First().FechaRegistro;
                    DateTime? fechaMaxIntensidadCaudal = maxIntensidadCaudal.HasValue ? climas.Where(c => c.IntensidadCaudal == maxIntensidadCaudal).First().FechaRegistro : null;

                    // Fechas de valores mínimos
                    DateTime fechaMinTemperatura = climas.Where(c => c.Temperatura == minTemperatura).First().FechaRegistro;
                    DateTime fechaMinIntensidadLuz = climas.Where(c => c.IntensidadLuz == minIntensidadLuz).First().FechaRegistro;
                    DateTime fechaMinIntensidadViento = climas.Where(c => c.IntensidadViento == minIntensidadViento).First().FechaRegistro;
                    DateTime? fechaMinIntensidadCaudal = minIntensidadCaudal.HasValue ? climas.Where(c => c.IntensidadCaudal == minIntensidadCaudal).First().FechaRegistro : null;

                    double temperaturaMediana = _calculadorEstadistico.Mediana(climas.Select(c => c.Temperatura));
                    double intensidadLuzMediana = _calculadorEstadistico.Mediana(climas.Select(c => c.IntensidadLuz));
                    double intensidadVientoMediana = _calculadorEstadistico.Mediana(climas.Select(c => c.IntensidadViento));
                    double? intensidadCaudalMediana = _calculadorEstadistico.Mediana(climas.Select(c => c.IntensidadCaudal));

                    double temperaturaPromedio = _calculadorEstadistico.Promedio(climas.Select(c => c.Temperatura));
                    double intensidadLuzPromedio = _calculadorEstadistico.Promedio(climas.Select(c => c.IntensidadLuz));
                    double intensidadVientoPromedio = _calculadorEstadistico.Promedio(climas.Select(c => c.IntensidadViento));
                    double? intensidadCaudalPromedio = _calculadorEstadistico.Promedio(climas.Select(c => c.IntensidadCaudal));

                    double temperaturaModa = _calculadorEstadistico.Moda(climas.Select(c => c.Temperatura));
                    double intensidadLuzModa = _calculadorEstadistico.Moda(climas.Select(c => c.IntensidadLuz));
                    double intensidadVientoModa = _calculadorEstadistico.Moda(climas.Select(c => c.IntensidadViento));
                    double? intensidadCaudalModa = _calculadorEstadistico.Moda(climas.Select(c => c.IntensidadCaudal));

                    ReporteClima reporteTemperatura = new ReporteClima()
                    {
                        Medida = "Temperatura",
                        UnidadMedida = "°C",
                        Promedio = temperaturaPromedio,
                        Mediana = temperaturaMediana,
                        Moda = temperaturaModa,
                        Max = maxTemperatura,
                        Min = minTemperatura,
                        FechaMax = fechaMaxTemperatura,
                        FechaMin = fechaMinTemperatura,
                    };
                    reportesClima.Add(reporteTemperatura);

                    ReporteClima reporteIntensidadLuz = new ReporteClima()
                    {
                        Medida = "Intensidad de luz",
                        UnidadMedida = "Lx",
                        Promedio = intensidadLuzPromedio,
                        Mediana = intensidadLuzMediana,
                        Moda = intensidadLuzModa,
                        Max = maxIntensidadLuz,
                        Min = minIntensidadLuz,
                        FechaMax = fechaMaxIntensidadLuz,
                        FechaMin = fechaMinIntensidadLuz
                    };
                    reportesClima.Add(reporteIntensidadLuz);

                    ReporteClima reporteIntensidadViento = new ReporteClima()
                    {
                        Medida = "Intensidad de viento",
                        UnidadMedida = "m/s",
                        Promedio = intensidadVientoPromedio,
                        Mediana = intensidadVientoMediana,
                        Moda = intensidadVientoModa,
                        Max = maxIntensidadViento,
                        Min = minIntensidadViento,
                        FechaMax = fechaMaxIntensidadViento,
                        FechaMin = fechaMinIntensidadViento
                    };
                    reportesClima.Add(reporteIntensidadViento);

                    if (existsIntensidadCaudal)
                    {
                        ReporteClima reporteIntensidadCaudal = new ReporteClima()
                        {
                            Medida = "Intensidad de caudal",
                            UnidadMedida = "m/s",
                            Promedio = intensidadCaudalPromedio!.Value,
                            Mediana = intensidadCaudalMediana!.Value,
                            Moda = intensidadCaudalModa!.Value,
                            Max = maxIntensidadCaudal!.Value,
                            Min = minIntensidadCaudal!.Value,
                            FechaMax = fechaMaxIntensidadCaudal!.Value,
                            FechaMin = fechaMinIntensidadCaudal!.Value
                        };
                        reportesClima.Add(reporteIntensidadCaudal);
                    }

                    return reportesClima;
                }
                else
                {
                    throw new ResourceNotFoundException("No existen climas");
                }
            }
            else
            {
                throw new BadRequestException("Id de ubicación no válido");
            }
        }

        public List<Clima> ObtenerClimasDetalle()
        {
            List<Clima> climas = _repository.ObtenerClimasDetalle(null);
            if (climas.Any())
            {
                return climas;
            }
            else
            {
                throw new ResourceNotFoundException("No existen climas");
            }
        }

        public Clima ObtenerClimaDetalle(int id)
        {
            if (id > 0)
            {
                List<Clima> climas = _repository.ObtenerClimasDetalle(id);
                if (climas.Any())
                {
                    return climas.First();
                }
                else
                {
                    throw new ResourceNotFoundException("No existe el clima buscado");
                }
            }
            else
            {
                throw new BadRequestException("Id de clima no válido");
            }
        }

        public List<Clima> ObtenerClimasFormulario(int idUsuario, DateTime fechaInicio, DateTime fechaFin, int? idUbicacion = null)
        {
            if (idUsuario > 0)
            {
                List<Clima> climas = _repository.ObtenerClimasFormulario(idUsuario, fechaInicio, fechaFin, idUbicacion);
                if (climas.Any())
                {
                    return climas;
                }
                else
                {
                    throw new ResourceNotFoundException("No existen climas");
                }
            }
            else
            {
                throw new BadRequestException("Id de ubicación no válido");
            }
        }

        public List<Clima> ObtenerClimasPorUbicacion(int idUbicacion)
        {
            if (idUbicacion > 0)
            {
                List<Clima> climas = _repository.ObtenerClimasPorUbicacion(idUbicacion);
                if (climas.Any())
                {
                    return climas;
                }
                else
                {
                    throw new ResourceNotFoundException("No existen climas");
                }
            }
            else
            {
                throw new BadRequestException("Id de ubicación no válido");
            }
        }

        public List<Clima> ObtenerClimasPorUsuario(int idUsuario)
        {
            if (idUsuario > 0)
            {
                List<Clima> climas = _repository.ObtenerClimasPorUsuario(idUsuario);
                if (climas.Any())
                {
                    return climas;
                }
                else
                {
                    throw new ResourceNotFoundException("No existen climas");
                }
            }
            else
            {
                throw new BadRequestException("Id de usuario no válido");
            }
        }

        public async Task<Clima?> ObtenerDatosSensorClima(int idUbicacion)
        {
            Clima? datosSensorClima = await _sensorClient.ObtenerDatosClima(idUbicacion);
            return datosSensorClima;
        }

        public Clima RegistrarClima(Clima clima)
        {
            Clima? climaRegistrado = _repository.RegistrarClima(clima);
            if (climaRegistrado != null)
            {
                return climaRegistrado;
            }
            else
            {
                throw new NotCreatedException("No se creó el clima solicitado");
            }
        }

        public int EliminarClima(int id)
        {
            int idClimaDeleted = _repository.EliminarClima(id);
            if (idClimaDeleted > 0)
            {
                return idClimaDeleted;
            }
            else
            {
                throw new ResourceNotFoundException("Clima indicado no existe");
            }
        }
    }
}
