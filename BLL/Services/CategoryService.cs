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
    public class CategoryService : ICategoryService
    {
        private readonly IUnitWork _unitWork;
        private readonly IMapper _mapper;

        public CategoryService(IUnitWork unitWork, IMapper mapper)
        {
            _unitWork = unitWork;
            _mapper = mapper;
        }

        public async Task Actualizar(CategoryDto modeloDto)
        {
            try
            {
                var categoryDb = await _unitWork.Category.ObtenerPrimero(e => e.Id == modeloDto.Id);
                if (categoryDb == null)
                    throw new TaskCanceledException("La Silla no Existe");

                categoryDb.Name = modeloDto.Name;
                categoryDb.Description = modeloDto.Description;
                categoryDb.Status = modeloDto.Status;
                
                _unitWork.Category.Actualizar(categoryDb);
                await _unitWork.Guardar();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<CategoryDto> Agregar(CategoryDto modeloDto)
        {
            try
            {
                Category category = new Category
                {
                    Name = modeloDto.Name,
                    Description = modeloDto.Description,
                    Status = modeloDto.Status
                    //FechaCreacion = DateTime.Now,
                    //FechaActualizacion = DateTime.Now
                };
                await _unitWork.Category.Agregar(category);
                await _unitWork.Guardar();
                if (category.Id == 0)
                    throw new TaskCanceledException("La Categoria no se pudo Crear");
                return _mapper.Map<CategoryDto>(category);

            }
            catch (Exception)
            {

                throw;
            }
        }

        
        public async Task<IEnumerable<CategoryDto>> ObtenerEstado()
        {
            try
            {
                // Obtener todas las sillas, tanto ocupadas como desocupadas
                var lista = await _unitWork.Category.ObtenerTodos(orderby: e => e.OrderBy(e => e.Name));

                // Si no se encuentran sillas, podrías devolver una lista vacía o lanzar una excepción, según sea necesario
                if (lista == null || !lista.Any())
                {
                    return Enumerable.Empty<CategoryDto>(); // Retorna una lista vacía si no hay sillas
                }

                // Mapea las sillas a ChairDto
                return _mapper.Map<IEnumerable<CategoryDto>>(lista);
            }
            catch (Exception ex)
            {
                // Maneja la excepción adecuadamente
                // Podrías registrar el error aquí si es necesario
                throw new Exception("Error al obtener el estado de la Categoria: " + ex.Message, ex);
            }
        }



        public async Task<IEnumerable<CategoryDto>> ObtenerTodos()
        {
            try
            {

                var lista = await _unitWork.Category.ObtenerTodos(
                            orderby: e => e.OrderBy(e => e.Name));
                return _mapper.Map<IEnumerable<CategoryDto>>(lista);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task Remover(int id)
        {
            var categoryDb = await _unitWork.Category.ObtenerPrimero(e => e.Id == id);
            if (categoryDb == null) throw new TaskCanceledException("La Categoria no Existe");
            _unitWork.Category.Remover(categoryDb);
            await _unitWork.Guardar();
        }
    }
}
