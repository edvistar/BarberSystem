using AutoMapper;
using BLL.Services.Interfaces;
using Data.Interfaces.IRepository;
using Model.Entities;
using Models.DTOs;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class OrdenService : IOrdenService
    {
        private readonly IUnitWork _unitWork;
        private readonly IMapper _mapper;

        public OrdenService(IUnitWork unitWork, IMapper mapper)
        {
            _unitWork = unitWork;
            _mapper = mapper;
        }

        public async Task Actualizar(OrdenDto modeloDto)
        {
            try
            {
                var ordenDb = await _unitWork.Orden.ObtenerPrimero(e => e.Id == modeloDto.Id);
                if (ordenDb == null)
                    throw new TaskCanceledException("La Orde no Existe");

                ordenDb.Numero = modeloDto.Numero;
                ordenDb.Servicios = modeloDto.Servicios;
                ordenDb.NombreCliente = modeloDto.NombreCliente;
                ordenDb.UsuarioAtiende = modeloDto.UsuarioAtiende;
                _unitWork.Orden.Actualizar(ordenDb);
                await _unitWork.Guardar();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<OrdenDto> Agregar(OrdenDto modeloDto)
        {
            try
            {
                Orden orden = new Orden
                {
                    Numero = modeloDto.Numero,
                    Servicios = modeloDto.Servicios,
                    NombreCliente = modeloDto.NombreCliente,
                    UsuarioAtiende = modeloDto.UsuarioAtiende

                    //FechaCreacion = DateTime.Now,
                    //FechaActualizacion = DateTime.Now
                };
                await _unitWork.Orden.Agregar(orden);
                await _unitWork.Guardar();
                if (orden.Id == 0)
                    throw new TaskCanceledException("El servicio no se pudo Crear");
                return _mapper.Map<OrdenDto>(orden);

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<OrdenDto>> ObtenerTodos()
        {
            try
            {

                var lista = await _unitWork.Orden.ObtenerTodos(
                            orderby: e => e.OrderBy(e => e.Numero));
                return _mapper.Map<IEnumerable<OrdenDto>>(lista);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task Remover(int id)
        {
            var ordenDb = await _unitWork.Orden.ObtenerPrimero(e => e.Id == id);
            if (ordenDb == null) throw new TaskCanceledException("La Silla no Existe");
            _unitWork.Orden.Remover(ordenDb);
            await _unitWork.Guardar();
        }

        public async Task AddServicesChair(int chairId, int serviceId)
        {
            try
            {
                var silla = await _unitWork.Chair.ObtenerPrimero(s => s.Id == chairId);
                if (silla == null)
                {
                    throw new TaskCanceledException("La silla no existe.");
                }

                var servicio = await _unitWork.Service.ObtenerPrimero(s => s.Id == serviceId);
                if (servicio == null)
                {
                    throw new TaskCanceledException("El servicio no existe.");
                }

                // Verificar si ya existe la relación entre silla y servicio
                var chairserviceExistente = await _unitWork.ChairServices.ObtenerPrimero(ss => ss.ChairId == chairId && ss.ServiceId == serviceId);
                if (chairserviceExistente != null)
                {
                    throw new TaskCanceledException("El servicio ya está asociado a esta silla.");
                }

                // Crear la relación Silla-Servicio
                var nuevaRelacion = new ChairServices
                {
                    ChairId = chairId,
                    ServiceId = serviceId
                };

                await _unitWork.ChairServices.Agregar(nuevaRelacion);
                await _unitWork.Guardar();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task RemoveServiceFromChair(int chairId, int serviceId)
        {
            try
            {
                // Buscar la relación existente entre la silla y el servicio
                var chairService = await _unitWork.ChairServices.ObtenerPrimero(ss => ss.ChairId == chairId && ss.ServiceId == serviceId);

                // Verificar si la relación existe
                if (chairService == null)
                {
                    throw new TaskCanceledException("La relación entre la silla y el servicio no existe.");
                }

                // Eliminar la relación
                _unitWork.ChairServices.Remover(chairService);
                await _unitWork.Guardar();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<ServiceDto>> ObtenerServiciosPorSilla(int chairId)
        {
            try
            {
                // Obtener la relación de servicios con la silla
                var chairServices = await _unitWork.ChairServices.ObtenerTodos(cs => cs.ChairId == chairId, incluirPropiedades: "Service");

                // Verificar si hay servicios asociados
                if (chairServices == null || !chairServices.Any())
                {
                    throw new TaskCanceledException("No hay servicios asociados a esta silla.");
                }

                // Mapear los servicios a DTO
                var servicios = chairServices.Select(cs => cs.Service);
                return _mapper.Map<IEnumerable<ServiceDto>>(servicios);
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
