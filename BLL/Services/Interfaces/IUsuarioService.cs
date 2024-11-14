using Model.DTOs;
using Models.DTOs;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Interfaces
{
    public interface IUsuarioService
    {
        Task<IEnumerable<UsuarioAplicacion>> ObtenerTodos();
        Task<UsuarioAplicacion> Agregar(UsuarioAplicacion modeloDto);
        Task Actualizar(UsuarioUpdateDto modeloDto);
        Task Remover(int id);
    }
}
