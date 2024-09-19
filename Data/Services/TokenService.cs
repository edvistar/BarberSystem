using Data.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Model.Entities;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Data.Services
{
    public class TokenService : ITokenService
    {
        private readonly UserManager<UsuarioAplicacion> _userManager;
        private readonly SymmetricSecurityKey _key;

        public TokenService(IConfiguration  config, UserManager<UsuarioAplicacion> userManage)
        {
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"]));
            _userManager = userManage;
        }

        public async Task<string> CrearToken(UsuarioAplicacion usuario)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.NameId, usuario.UserName)
            };

            var roles = await _userManager.GetRolesAsync(usuario);
            claims.AddRange(roles.Select(rol => new Claim(ClaimTypes.Role, rol)));
            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = creds,
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
