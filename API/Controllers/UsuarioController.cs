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

        public UsuarioController(UserManager<UsuarioAplicacion> userManager, ITokenService tokenService,
            RoleManager<RolAplicacion> roleManager)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _rolManager = roleManager;
            _response = new ApiResponse();
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuarios()
        {
            var usuarios = await _userManager.Users.ToListAsync();
            return Ok(usuarios);
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


        [HttpPost("registro")] 
        public async Task<ActionResult<UsuarioDto>> Registro(RegistroDto registroDto)
        {
            if (await UsuarioExiste(registroDto.Username)) return BadRequest("UserName ya esta Registrado");

            var usuario = new UsuarioAplicacion
            {
                UserName = registroDto.Username.ToLower(),
                Email = registroDto.Email.ToLower(),
                Apellidos = registroDto.Apellidos,
                Nombres = registroDto.Nombres,
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
    }
}

