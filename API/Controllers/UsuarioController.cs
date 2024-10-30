using BLL.Services;
using BLL.Services.Interfaces;
using Data.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Model.DTOs;
using Model.Entities;
using Models.DTOs;
using Models.Entities;
using System.Net;

namespace API.Controllers
{
    public class UsuarioController : BaseApiController
    {
        private readonly UserManager<UsuarioAplicacion> _userManager;
        private readonly ITokenService _tokenService;
        private ApiResponse _response;
        private readonly RoleManager<RolAplicacion> _rolManager;
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(UserManager<UsuarioAplicacion> userManager, ITokenService tokenService,
            RoleManager<RolAplicacion> roleManager , IUsuarioService usuarioService
            )
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _rolManager = roleManager;
            _response = new ApiResponse();
            _usuarioService = usuarioService;
        }


        [Authorize(Policy = "AdminRol")]
        [HttpGet]
        public async Task<ActionResult> GetUsuarios()
        {
            var usuarios = await _userManager.Users.ToListAsync();  // Obtener todos los usuarios en memoria primero

            var usuarioListaDtos = new List<UsuarioListaDto>();

            foreach (var u in usuarios)
            {
                var roles = await _userManager.GetRolesAsync(u);  // Obtener roles asincrónicamente
                usuarioListaDtos.Add(new UsuarioListaDto
                {
                    Id = u.Id,
                    UserName = u.UserName,
                    Apellidos = u.Apellidos,
                    Nombres = u.Nombres,
                    Documento = u.Documento,
                    Address = u.Address,
                    PhoneNumber = u.PhoneNumber,
                    Email = u.Email,

                    Rol = string.Join(",", roles)  // Convertir lista de roles a una cadena
                });
            }

            _response.Resultado = usuarioListaDtos;
            _response.IsExitoso = true;
            _response.StatusCode = HttpStatusCode.OK;
            return Ok(_response);
        }

        [Authorize(Policy = "AdminRol")]
        // [ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet("ListadoClientes")]
        public async Task<ActionResult> ListadoClientes()
        {
            try
            {
                var usuarios = await _userManager.Users.ToListAsync();
                var clientesDto = new List<UsuarioClienteDto>();

                foreach (var u in usuarios)
                {
                    var roles = await _userManager.GetRolesAsync(u);
                    if (roles.Contains("Cliente"))
                    {
                        clientesDto.Add(new UsuarioClienteDto
                        {
                            Id = u.Id,
                            Nombres = u.Nombres,
                            Apellidos=u.Apellidos
                        });
                    }
                }
                _response.Resultado = clientesDto;
                _response.IsExitoso = true;
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);

                
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> GetUsuario(int id)
        {
            var usuario = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (usuario == null)
            {
                return NotFound();
            }
            return Ok(usuario);
        }

        [Authorize(Policy ="AdminRol")]
        [HttpPost("registro")] 
        public async Task<ActionResult<UsuarioDto>> Registro(UsuarioRegistroDto registroDto)
        {
            if (await UsuarioExiste(registroDto.Username)) return BadRequest("UserName ya esta Registrado");

            var usuario = new UsuarioAplicacion
            {
                UserName = registroDto.Username.ToLower(),
                Email = registroDto.Email.ToLower(),
                Apellidos = registroDto.Apellidos,
                Nombres = registroDto.Nombres,
                Documento = registroDto.Documento,
                Address = registroDto.Address,
                PhoneNumber = registroDto.PhoneNumber,
            };
            var resultado = await _userManager.CreateAsync(usuario, registroDto.Password);
            if (!resultado.Succeeded) return BadRequest(resultado.Errors);

            var rolResultado = await _userManager.AddToRoleAsync(usuario, registroDto.Rol);
            if (!resultado.Succeeded) return BadRequest("Error al agregar el rol del usuario");
            return new UsuarioDto
            {
                UserName = usuario.UserName,
                Token = await _tokenService.CrearToken(usuario)
            };
        }

        
        [Authorize(Policy = "AdminRol")]
        [HttpPut("Actualizar")]
        public async Task<ActionResult<UsuarioAplicacion>> Actualizar(UsuarioUpdateDto modeloDto)
        {
            var usuario = await _userManager.Users.FirstOrDefaultAsync(p => p.Id == modeloDto.Id);
            if (usuario == null) return NotFound("Usuario no encontrado");

            usuario.UserName = modeloDto.UserName.ToLower();
            usuario.Email = modeloDto.Email.ToLower();
            usuario.Apellidos = modeloDto.Apellidos;
            usuario.Nombres = modeloDto.Nombres;
            usuario.Documento = modeloDto.Documento;
            usuario.Address = modeloDto.Address;
            usuario.PhoneNumber = modeloDto.PhoneNumber;

            var resultado = await _userManager.UpdateAsync(usuario);
            if (!resultado.Succeeded) return BadRequest(resultado.Errors);

            var rolesActuales = await _userManager.GetRolesAsync(usuario);
            if (rolesActuales.Count > 0)
            {
                var eliminarRoles = await _userManager.RemoveFromRolesAsync(usuario, rolesActuales);
                if (!eliminarRoles.Succeeded) return BadRequest("Error al eliminar los roles antiguos del usuario");
            }

            var agregarRol = await _userManager.AddToRoleAsync(usuario, modeloDto.Rol);
            if (!agregarRol.Succeeded) return BadRequest("Error al agregar el nuevo rol al usuario");

            // Retornar el objeto completo con todas las propiedades para el frontend
            return Ok(new UsuarioUpdateDto
            {
                Id = usuario.Id,
                UserName = usuario.UserName,
                Apellidos = usuario.Apellidos,
                Nombres = usuario.Nombres,
                Documento = usuario.Documento,
                Email = usuario.Email,
                Address = usuario.Address,
                PhoneNumber = usuario.PhoneNumber,
                Rol = modeloDto.Rol,
                //Token = await _tokenService.CrearToken(usuario)
            });
        }


        [HttpPost("login")]
        public async Task<ActionResult<UsuarioDto>> Login(LoginDto loginDto)
        {
            var usuario = await _userManager.Users.SingleOrDefaultAsync(x => x.UserName == loginDto.UserName);
            if (usuario == null) return Unauthorized("Usuario no Valido");
            var resultado = await _userManager.CheckPasswordAsync(usuario, loginDto.Password);

            if(!resultado) return Unauthorized("Password no valido");
            return new UsuarioDto
            {
                UserName = usuario.UserName,
                Name =usuario.Nombres,
                Token = await _tokenService.CrearToken(usuario)
            };
        }

        [HttpGet("ListadoRoles")]
        public IActionResult GetRoles()
        {
            var roles = _rolManager.Roles.Select(r => new { NombreRol = r.Name }).ToList();
            _response.Resultado = roles;
            _response.IsExitoso = true;
            _response.StatusCode = HttpStatusCode.OK;
            return Ok(_response);
        }
        private async Task<bool> UsuarioExiste(string username)
        {
            return await _userManager.Users.AnyAsync(x => x.UserName == username.ToLower());
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            try
            {
                await _usuarioService.Remover(id);
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

