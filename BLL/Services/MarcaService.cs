using AutoMapper;
using Azure;
using BLL.Services.Interfaces;
using Data.Interfaces.IRepository;
using Microsoft.EntityFrameworkCore;
using Model.Entities;
using Models.DTOs;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class MarcaService : IMarcaService
    {
        private readonly IUnitWork _unitWork;
        private readonly IMapper _mapper;

        public MarcaService(IUnitWork unitWork, IMapper mapper)
        {
            _unitWork = unitWork;
            _mapper = mapper;
        }

        public async Task Actualizar(MarcaDto modeloDto)
        {
            try
            {
                var marcaDb = await _unitWork.Marca.ObtenerPrimero(e => e.Id == modeloDto.Id);
                if (marcaDb == null)
                    throw new TaskCanceledException("La Silla no Existe");

                marcaDb.Name = modeloDto.Name;
                marcaDb.Status = modeloDto.Status;
                marcaDb.ImageUrl = modeloDto.ImageUrl;
                _unitWork.Marca.Actualizar(marcaDb);
                await _unitWork.Guardar();
            }
            catch (Exception)
            {

                throw;
            }
        }


        public async Task<MarcaDto> Agregar(MarcaDto modeloDto)
        {
            try
            {
                Marca marca = new Marca
                {
                    Name = modeloDto.Name,
                    Status = modeloDto.Status,
                    ImageUrl = modeloDto.ImageUrl,


                    //FechaCreacion = DateTime.Now,
                    //FechaActualizacion = DateTime.Now
                };
                await _unitWork.Marca.Agregar(marca);
                await _unitWork.Guardar();
                if (marca.Id == 0)
                    throw new TaskCanceledException("La Silla no se pudo Crear");
                return _mapper.Map<MarcaDto>(marca);

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<MarcaDto>> ObtenerTodos()
        {
            try
            {

                var lista = await _unitWork.Marca.ObtenerTodos(
                            orderby: e => e.OrderBy(e => e.Name));
                return _mapper.Map<IEnumerable<MarcaDto>>(lista);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task Remover(int id)
        {
            var marcaDb = await _unitWork.Marca.ObtenerPrimero(e => e.Id == id);
            if (marcaDb == null) throw new TaskCanceledException("La Marca no Existe");
            _unitWork.Marca.Remover(marcaDb);
            await _unitWork.Guardar();
        }
    }
}
