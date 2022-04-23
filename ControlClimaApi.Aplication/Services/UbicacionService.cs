using ControlClimaApi.Domain.Abstractions.Repositories;
using ControlClimaApi.Domain.Abstractions.Services;
using ControlClimaApi.Domain.Entities;
using ControlClimaApi.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlClimaApi.Aplication.Services
{
    public class UbicacionService : IUbicacionService
    {
        private readonly IUbicacionRepository _repository;

        public UbicacionService(IUbicacionRepository repository)
        {
            _repository = repository;
        }

        public List<Ubicacion> ObtenerUbicaciones()
        {
            List<Ubicacion> ubicaciones = _repository.ObtenerUbicaciones(null);
            if (ubicaciones.Any())
            {
                return ubicaciones;
            }
            else
            {
                throw new ResourceNotFoundException("No existen ubicaciones");
            }
        }

        public Ubicacion ObtenerUbicacion(int id)
        {
            if (id > 0)
            {
                List<Ubicacion> ubicaciones = _repository.ObtenerUbicaciones(id);
                if (ubicaciones.Any())
                {
                    return ubicaciones.First();
                }
                else
                {
                    throw new ResourceNotFoundException("No existe la ubicación buscada");
                }
            }
            else
            {
                throw new BadRequestException("Id de ubicación no válido");
            }
        }
    }
}
