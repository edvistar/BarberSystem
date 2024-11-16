using Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDto>> ObtenerTodos();
        Task<IEnumerable<CategoryDto>> ObtenerEstado();
        Task<CategoryDto> Agregar(CategoryDto modeloDto);
        Task Actualizar(CategoryDto modeloDto);
        Task Remover(int id);
    }
}
