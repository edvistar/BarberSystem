using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOs
{
    public class RegistroDto
    {
        [Required(ErrorMessage = "Username es Requerido")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Password es Requerido")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Apellido es Requerido")]
        public string Apellidos { get; set; }
        [Required(ErrorMessage = "Nombres son Requerido")]
        public string Nombres { get; set; }
        [Required(ErrorMessage = "Email es Requerido")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Rol es Requerido")]
        public string Rol { get; set; }
    }
}
