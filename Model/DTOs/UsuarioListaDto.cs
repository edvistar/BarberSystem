using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOs
{
    public class UsuarioListaDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Apellidos { get; set; }
        public string Nombres { get; set; }
        public int Documento { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Rol { get; set; }
    }
}
