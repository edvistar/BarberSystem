﻿using AutoMapper;
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
    public class ChairService : IChairService
    {
        private readonly IUnitWork _unitWork;
        private readonly IMapper _mapper;

        public ChairService(IUnitWork unitWork, IMapper mapper)
        {
            _unitWork = unitWork;
            _mapper = mapper;
        }

        public async Task Actualizar(ChairDto modeloDto)
        {
            try
            {
                var chairDb = await _unitWork.Chair.ObtenerPrimero(e => e.Id == modeloDto.Id);
                if (chairDb == null)
                    throw new TaskCanceledException("La Silla no Existe");

                chairDb.Name = modeloDto.Name;
                chairDb.Numero = modeloDto.Numero;
                chairDb.Logo = modeloDto.Logo;
                _unitWork.Chair.Actualizar(chairDb);
                await _unitWork.Guardar();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<ChairDto> Agregar(ChairDto modeloDto)
        {
            try
            {
                Chair chair = new Chair
                {
                    Name = modeloDto.Name,
                    Numero = modeloDto.Numero,
                    Logo = modeloDto.Logo

                    //FechaCreacion = DateTime.Now,
                    //FechaActualizacion = DateTime.Now
                };
                await _unitWork.Chair.Agregar(chair);
                await _unitWork.Guardar();
                if (chair.Id == 0)
                    throw new TaskCanceledException("La Silla no se pudo Crear");
                return _mapper.Map<ChairDto>(chair);

            }
            catch (Exception)
            {

                throw;
            }
        }

        //public async Task<IEnumerable<ChairDto>> ObtenerActivos()
        //{
        //    try
        //    {

        //        var lista = await _unitWork.Chair.ObtenerTodos(x => x.Estado == true,
        //                    orderby: e => e.OrderBy(e => e.Name));
        //        return _mapper.Map<IEnumerable<ChairDto>>(lista);
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}

        public async Task<IEnumerable<ChairDto>> ObtenerTodos()
        {
            try
            {

                var lista = await _unitWork.Chair.ObtenerTodos(
                            orderby: e => e.OrderBy(e => e.Name));
                return _mapper.Map<IEnumerable<ChairDto>>(lista);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task Remover(int id)
        {
            var chairDb = await _unitWork.Chair.ObtenerPrimero(e => e.Id == id);
            if (chairDb == null) throw new TaskCanceledException("La Silla no Existe");
            _unitWork.Chair.Remover(chairDb);
            await _unitWork.Guardar();
        }
    }
}