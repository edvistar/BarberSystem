using BLL.Services;
using BLL.Services.Interfaces;
using Data.Interfaces.IRepository;
using Microsoft.AspNetCore.Mvc;
using Models.DTOs;
using System.Net;

namespace API.Controllers
{
    public class MarcaController : BaseApiController
    {
        private readonly IMarcaService _marcaService;
        private ApiResponse _response;

        public MarcaController(IMarcaService marcaService)
        {
            _marcaService = marcaService;
            _response = new();
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                _response.Resultado = await _marcaService.ObtenerTodos();
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
        public async Task<IActionResult> Crear(MarcaDto modeloDto)
        {
            try
            {
                await _marcaService.Agregar(modeloDto);
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
        public async Task<IActionResult> Editar(MarcaDto  modeloDto)
        {
            try
            {
                await _marcaService.Actualizar(modeloDto);
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
                await _marcaService.Remover(id);
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
