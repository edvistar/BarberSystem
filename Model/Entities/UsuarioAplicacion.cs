﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entities
{
    public class UsuarioAplicacion : IdentityUser<int>
    {
        public string Apellidos { get; set; }
        public string Nombres { get; set; }
        public int Documento { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }

        public ICollection<RolUsuarioAplicacion> RolUsuarios { get; set; }
    }
}
