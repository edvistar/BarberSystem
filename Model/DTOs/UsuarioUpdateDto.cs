using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOs
{
    public class UsuarioUpdateDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Username es Requerido")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Password es Requerido")]
        public string Apellidos { get; set; }
        [Required(ErrorMessage = "Nombres son Requerido")]
        public string Nombres { get; set; }
        [Required(ErrorMessage = "Documento es Requerido")]
        public int Documento { get; set; }
        [Required(ErrorMessage = "Email es Requerido")]
        public string Email { get; set; }
        [Required(ErrorMessage = "La direccion es Requerida")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Rol es Requerido")]
        public string PhoneNumber { get; set; }
        public string Rol { get; set; }
    }
}
