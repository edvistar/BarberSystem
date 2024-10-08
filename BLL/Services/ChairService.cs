﻿using AutoMapper;
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
                chairDb.Ocuped = modeloDto.Ocuped == 1 ? true : false;
                _unitWork.Chair.Actualizar(chairDb);
                await _unitWork.Guardar();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task ActualizarEstado(ChairEstadoDto modeloDto)
        {

            try
            {
                var ocupedDb = await _unitWork.Chair.ObtenerPrimero(e => e.Id == modeloDto.Id);
                if (ocupedDb == null)
                    throw new TaskCanceledException("el estado no Existe");


                ocupedDb.Ocuped = modeloDto.Ocuped == 1 ? true : false;
                _unitWork.Chair.Actualizar(ocupedDb);
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
                    Logo = modeloDto.Logo,
                    Ocuped = modeloDto.Ocuped == 1 ? true : false,


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

        //public async Task<IEnumerable<ChairDto>> ObtenerEstado()
        //{
        //    try
        //    {

        //        var lista = await _unitWork.Chair.ObtenerTodos(x => x.Ocuped == true,
        //                    orderby: e => e.OrderBy(e => e.Name));
        //        return _mapper.Map<IEnumerable<ChairDto>>(lista);
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}

        public async Task<IEnumerable<ChairDto>> ObtenerEstado()
        {
            try
            {
                // Obtener todas las sillas, tanto ocupadas como desocupadas
                var lista = await _unitWork.Chair.ObtenerTodos(orderby: e => e.OrderBy(e => e.Name));

                // Si no se encuentran sillas, podrías devolver una lista vacía o lanzar una excepción, según sea necesario
                if (lista == null || !lista.Any())
                {
                    return Enumerable.Empty<ChairDto>(); // Retorna una lista vacía si no hay sillas
                }

                // Mapea las sillas a ChairDto
                return _mapper.Map<IEnumerable<ChairDto>>(lista);
            }
            catch (Exception ex)
            {
                // Maneja la excepción adecuadamente
                // Podrías registrar el error aquí si es necesario
                throw new Exception("Error al obtener el estado de las sillas: " + ex.Message, ex);
            }
        }



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
