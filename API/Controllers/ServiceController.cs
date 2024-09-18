using BLL.Services;
using BLL.Services;
using BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Models.DTOs;
using System.Net;

namespace API.Controllers
{
    public class ServiceController : BaseApiController
    {
        private readonly IServiceService _serviceService;
        private ApiResponse _response;

        public ServiceController(IServiceService serviceService)
        {
            _serviceService = serviceService;
            _response = new();
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                _response.Resultado = await _serviceService.ObtenerTodos();
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
        public async Task<IActionResult> Crear(ServiceDto modeloDto)
        {
            try
            {
                await _serviceService.Agregar(modeloDto);
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
        public async Task<IActionResult> Editar(ServiceDto modeloDto)
        {
            try
            {
                await _serviceService.Actualizar(modeloDto);
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
                await _serviceService.Remover(id);
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
