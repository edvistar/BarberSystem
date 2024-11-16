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
    public class ProductService : IProductService
    {
        private readonly IUnitWork _unitWork;
        private readonly IMapper _mapper;

        public ProductService(IUnitWork unitWork, IMapper mapper)
        {
            _unitWork = unitWork;
            _mapper = mapper;
        }

        public async Task Actualizar(ProductDto modeloDto)
        {
            try
            {
                var productDb = await _unitWork.Product.ObtenerPrimero(e => e.Id == modeloDto.Id);
                if (productDb == null)
                    throw new TaskCanceledException("La Silla no Existe");

                productDb.Name = modeloDto.Name;
                productDb.SerialNumber = modeloDto.SerialNumber;
                productDb.Description = modeloDto.Description;
                productDb.Status = modeloDto.Status;
                productDb.Offer = modeloDto.Offer;
                productDb.Price = modeloDto.Price;
                productDb.Cost = modeloDto.Cost;
                productDb.Categoria = modeloDto.Categoria;
                productDb.Marca = modeloDto.Marca;
                productDb.ImageUrl = modeloDto.ImageUrl;
                _unitWork.Product.Actualizar(productDb);
                await _unitWork.Guardar();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<ProductDto> Agregar(ProductDto modeloDto)
        {
            try
            {
                Product product = new Product
                {
                    Name = modeloDto.Name,
                    SerialNumber = modeloDto.SerialNumber,
                    Description = modeloDto.Description,
                    Status = modeloDto.Status,
                    Offer = modeloDto.Offer,
                    Price = modeloDto.Price,
                    Cost = modeloDto.Cost,
                    Categoria = modeloDto.Categoria,
                    Marca = modeloDto.Marca,
                    ImageUrl = modeloDto.ImageUrl,


                    //FechaCreacion = DateTime.Now,
                    //FechaActualizacion = DateTime.Now
                };
                await _unitWork.Product.Agregar(product);
                await _unitWork.Guardar();
                if (product.Id == 0)
                    throw new TaskCanceledException("El Producto no se pudo Crear");
                return _mapper.Map<ProductDto>(product);

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<ProductDto>> ObtenerTodos()
        {
            try
            {

                var lista = await _unitWork.Product.ObtenerTodos(
                            orderby: e => e.OrderBy(e => e.Name));
                return _mapper.Map<IEnumerable<ProductDto>>(lista);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task Remover(int id)
        {
            var productDb = await _unitWork.Product.ObtenerPrimero(e => e.Id == id);
            if (productDb == null) throw new TaskCanceledException("El Producto no Existe");
            _unitWork.Product.Remover(productDb);
            await _unitWork.Guardar();
        }
    }
}
