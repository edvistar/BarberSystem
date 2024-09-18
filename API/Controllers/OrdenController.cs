using BLL.Services;
using BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Models.DTOs;
using System.Net;

namespace API.Controllers
{
    public class OrdenController : BaseApiController
    {
        private readonly IOrdenService _ordenService;
        private ApiResponse _response;

        public OrdenController(IOrdenService ordenService)
        {
            _ordenService = ordenService;
            _response = new();
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                _response.Resultado = await _ordenService.ObtenerTodos();
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

        //[HttpGet("ListadoActivo")]
        //public async Task<IActionResult> GetActivos()
        //{
        //    try
        //    {
        //        _response.Resultado = await _chairService.ObtenerActivos();
        //        _response.IsExitoso = true;
        //        _response.StatusCode = HttpStatusCode.OK;

        //    }
        //    catch (Exception ex)
        //    {

        //        _response.IsExitoso = false;
        //        _response.Mensaje = ex.Message;
        //        _response.StatusCode = HttpStatusCode.BadRequest;
        //    }
        //    return Ok(_response);
        //}

        [HttpPost]
        public async Task<IActionResult> Crear(OrdenDto modeloDto)
        {
            try
            {
                await _ordenService.Agregar(modeloDto);
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
        public async Task<IActionResult> Editar(OrdenDto modeloDto)
        {
            try
            {
                await _ordenService.Actualizar(modeloDto);
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
                await _ordenService.Remover(id);
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
