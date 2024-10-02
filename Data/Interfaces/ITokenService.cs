using Model.Entities;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interfaces
{
    public interface ITokenService
    {
        Task<string> CrearToken(UsuarioAplicacion usuario);
    }
}
