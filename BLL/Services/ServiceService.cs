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
    public class ServiceService : IServiceService
    {
        private readonly IUnitWork _unitWork;
        private readonly IMapper _mapper;

        public ServiceService(IUnitWork unitWork, IMapper mapper)
        {
            _unitWork = unitWork;
            _mapper = mapper;
        }

        public async Task Actualizar(ServiceDto modeloDto)
        {
            try
            {
                var serviceDb = await _unitWork.Service.ObtenerPrimero(e => e.Id == modeloDto.Id);
                if (serviceDb == null)
                    throw new TaskCanceledException("El servicio no Existe");

                serviceDb.Name = modeloDto.Name;
                serviceDb.Description = modeloDto.Description;
                serviceDb.Price = modeloDto.Price;
                _unitWork.Service.Actualizar(serviceDb);
                await _unitWork.Guardar();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<ServiceDto> Agregar(ServiceDto modeloDto)
        {
            try
            {
                Service service = new Service
                {
                    Name = modeloDto.Name,
                    Description = modeloDto.Description,
                    Price = modeloDto.Price,

                    //FechaCreacion = DateTime.Now,
                    //FechaActualizacion = DateTime.Now
                };
                await _unitWork.Service.Agregar(service);
                await _unitWork.Guardar();
                if (service.Id == 0)
                    throw new TaskCanceledException("El servicio no se pudo Crear");
                return _mapper.Map<ServiceDto>(service);

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<ServiceDto>> ObtenerTodos()
        {
            try
            {

                var lista = await _unitWork.Service.ObtenerTodos(
                            orderby: e => e.OrderBy(e => e.Name));
                return _mapper.Map<IEnumerable<ServiceDto>>(lista);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task Remover(int id)
        {
            var serviceDb = await _unitWork.Service.ObtenerPrimero(e => e.Id == id);
            if (serviceDb == null) throw new TaskCanceledException("La Silla no Existe");
            _unitWork.Service.Remover(serviceDb);
            await _unitWork.Guardar();
        }
    }
}
