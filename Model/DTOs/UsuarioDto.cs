using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOs
{
    public class UsuarioDto
    {
        public int Id { get; set; }
        public string? UserName { get; set; }
        public string Name { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string Rol { get; set; }
        public string Token { get; set; }
    }
}
