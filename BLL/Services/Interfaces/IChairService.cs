using Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Interfaces
{
    public interface IChairService
    {
        Task<IEnumerable<ChairDto>> ObtenerTodos();
        Task<IEnumerable<ChairDto>> ObtenerEstado();
        Task<ChairDto> Agregar(ChairDto modeloDto);
        Task Actualizar(ChairDto modeloDto);
        Task ActualizarEstado(ChairEstadoDto modeloDto);
        Task Remover(int id);
    }
}
