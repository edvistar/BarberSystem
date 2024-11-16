using BLL.Services;
using BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Models.DTOs;
using System.Net;

namespace API.Controllers
{
    public class CategoryController : BaseApiController
    {
        private readonly ICategoryService _categoyService;
        private ApiResponse _response;

        public CategoryController(ICategoryService categoyService)
        {
            _categoyService = categoyService;
            _response = new();
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                _response.Resultado = await _categoyService.ObtenerTodos();
                _response.IsExitoso = true;
                _response.StatusCode = HttpStatusCode.OK;

            }
            catch (Exception ex)
            {

                _response.IsExitoso = false;
                _response.Mensaje = ex.Message;
                _response.StatusCode = HttpStatusCode.BadRequest;
            }
            return Ok(_response);
        }

   

        [HttpPost]
        public async Task<IActionResult> Crear(CategoryDto modeloDto)
        {
            try
            {
                await _categoyService.Agregar(modeloDto);
                _response.IsExitoso = true;
                _response.StatusCode = HttpStatusCode.OK;
            }
            catch (Exception ex)
            {

                _response.IsExitoso = false;
                _response.Resultado = ex.Message;
                _response.StatusCode = HttpStatusCode.BadRequest;
            }
            return Ok(_response);
        }

        [HttpPut]
        public async Task<IActionResult> Editar(CategoryDto  modeloDto)
        {
            try
            {
                await _categoyService.Actualizar(modeloDto);
                _response.IsExitoso = true;
                _response.StatusCode = HttpStatusCode.NoContent;
            }
            catch (Exception ex)
            {

                _response.IsExitoso = false;
                _response.Resultado = ex.Message;
                _response.StatusCode = HttpStatusCode.BadRequest;
            }
            return Ok(_response);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            try
            {
                await _categoyService.Remover(id);
                _response.IsExitoso = true;
                _response.StatusCode = HttpStatusCode.NoContent;
            }
            catch (Exception ex)
            {

                _response.IsExitoso = false;
                _response.Mensaje = ex.Message;
                _response.StatusCode = HttpStatusCode.BadRequest;
            }
            return Ok(_response);
        }
    }
}
