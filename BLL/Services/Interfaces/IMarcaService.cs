using Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Interfaces
{
    public interface IMarcaService
    {
        Task<IEnumerable<MarcaDto>> ObtenerTodos();
        Task<MarcaDto> Agregar(MarcaDto modeloDto);
        Task Actualizar(MarcaDto modeloDto);
        Task Remover(int id);
    }
}
