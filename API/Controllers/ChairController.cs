using BLL.Services;
using BLL.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.DTOs;
using System.Net;

namespace API.Controllers
{
    public class ChairController : BaseApiController
    {
        private readonly IChairService _chairService;
        private ApiResponse _response;

        public ChairController(IChairService chairService)
        {
            _chairService = chairService;
            _response = new();
        }
        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                _response.Resultado = await _chairService.ObtenerTodos();
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

        [HttpGet("GetEstado")]
        public async Task<IActionResult> GetEstado()
        {
            try
            {
                _response.Resultado = await _chairService.ObtenerEstado();
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
        public async Task<IActionResult> Crear(ChairDto modeloDto)
        {
            try
            {
                await _chairService.Agregar(modeloDto);
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
        public async Task<IActionResult> Editar(ChairDto modeloDto)
        {
            try
            {
                await _chairService.Actualizar(modeloDto);
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

        [HttpPatch("EditarEstado")]
        public async Task<IActionResult> EditarEstado([FromBody] ChairEstadoDto modeloDto)
        {
            try
            {
                await _chairService.ActualizarEstado(modeloDto);
                _response.IsExitoso = true;
                _response.StatusCode = HttpStatusCode.NoContent;

                // Validar que el ID sea válido
                //if (modeloDto == null || modeloDto.Id <= 0)
                //{
                //    _response.IsExitoso = false;
                //    _response.Resultado = "Datos inválidos";
                //    _response.StatusCode = HttpStatusCode.BadRequest;
                //    return BadRequest(_response);
                //}

                //// Llamar al servicio para actualizar el estado de la silla
                //await _chairService.ActualizarEstado(modeloDto);

                //_response.IsExitoso = true;
                //_response.StatusCode = HttpStatusCode.NoContent;
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
                await _chairService.Remover(id);
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
