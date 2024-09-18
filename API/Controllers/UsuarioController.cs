using Data;
using Data.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Model.DTOs;
using Model.Entities;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using OfficeOpenXml;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace API.Controllers
{
    public class UsuarioController : BaseApiController
    {
        private readonly ApplicationDbContext _db;
        private readonly ITokenService _tokenService;

        public UsuarioController(ApplicationDbContext db, ITokenService tokenService)
        {
            _db = db;
            _tokenService = tokenService;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuarios()
        {
            var usuarios = await _db.Usuarios.ToListAsync();
            return Ok(usuarios);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> GetUsuario(int id)
        {
            var usuario = await _db.Usuarios.FindAsync(id);
            return Ok(usuario);
        }
         
        [HttpPost("registro")] 
        public async Task<ActionResult<UsuarioDto>> Registro(RegistroDto registroDto)
        {
            if (await UsuarioExiste(registroDto.Username)) return BadRequest("UserName ya esta Registrado");

            //using var hmac = new HMACSHA512();
            var usuario = new Usuario
            {
                UserName = registroDto.Username.ToLower(),
               // PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registroDto.Password)),
               // PasswordSalt = hmac.Key
            };
            _db.Usuarios.Add(usuario);
            await _db.SaveChangesAsync();

            return new UsuarioDto
            {
                UserName = usuario.UserName,
                Token = _tokenService.CrearToken(usuario)
            };
        }

        [HttpPost("login")]
        public async Task<ActionResult<UsuarioDto>> Login(LoginDto loginDto)
        {
            var usuario = await _db.Usuarios.SingleOrDefaultAsync(x => x.UserName == loginDto.UserName);
            if (usuario == null) return Unauthorized("Usuario no Valido");
            //using var hmac = new HMACSHA512(usuario.PasswordSalt);
            //var compuHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));
            //for (var i = 0; i < compuHash.Length; i++)
            //{
            //    if (compuHash[i] != usuario.PasswordHash[i]) return Unauthorized("Password no valido");
            //}
            return new UsuarioDto
            {
                UserName = usuario.UserName,
                Token = _tokenService.CrearToken(usuario)
            };
        }

        private async Task<bool> UsuarioExiste(string username)
        {
            return await _db.Usuarios.AnyAsync(x => x.UserName == username.ToLower());
        }

        [HttpPost("upload")]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded.");

            var usuarios = new List<Usuario>();

            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial; // Establecer el contexto de la licencia
                using (var package = new ExcelPackage(stream))
                {
                    var worksheet = package.Workbook.Worksheets.First();
                    var rowCount = worksheet.Dimension.Rows;

                    for (int row = 2; row <= rowCount; row++)
                    {
                        using var hmac = new HMACSHA512();
                        var usuario = new Usuario
                        {
                            UserName = worksheet.Cells[row, 1].Value.ToString().Trim().ToLower(),
                            PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(worksheet.Cells[row, 2].Value.ToString().Trim())),
                            PasswordSalt = hmac.Key
                        };
                        usuarios.Add(usuario);
                    }
                }
            }

            _db.Usuarios.AddRange(usuarios);
            await _db.SaveChangesAsync();

            return Ok("File uploaded and data saved.");
        }

        [HttpPost("uploadtwo")]
        public async Task<IActionResult> Uploadtwo(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded.");

            var usuarios = new List<Usuario>();
            // Obtener la lista de nombres de usuario existentes en la base de datos
            var existingUsernamesList = await _db.Usuarios.Select(u => u.UserName).ToListAsync();
            var existingUsernames = new HashSet<string>(existingUsernamesList);

            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);

                stream.Position = 0;
                IWorkbook workbook = new XSSFWorkbook(stream);
                ISheet sheet = workbook.GetSheetAt(0);
                var rowCount = sheet.LastRowNum;

                for (int row = 1; row <= rowCount; row++)
                {
                    IRow currentRow = sheet.GetRow(row);
                    if (currentRow == null) continue;

                    var username = currentRow.GetCell(0)?.ToString().Trim().ToLower();

                    // Verificar si el nombre de usuario ya existe
                    if (existingUsernames.Contains(username))
                    {
                        return BadRequest($"El nombre de usuario '{username}' ya está registrado.");
                    }
                    using var hmac = new HMACSHA512();
                    var usuario = new Usuario
                    {
                        UserName = currentRow.GetCell(0).ToString().Trim().ToLower(),
                        PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(currentRow.GetCell(1).ToString().Trim())),
                        PasswordSalt = hmac.Key
                    };
                    usuarios.Add(usuario);
                }
            }

            _db.Usuarios.AddRange(usuarios);
            await _db.SaveChangesAsync();

            return Ok("File uploaded and data saved.");
        }
    }

}

