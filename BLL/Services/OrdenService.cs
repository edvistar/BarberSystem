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

    }
}
