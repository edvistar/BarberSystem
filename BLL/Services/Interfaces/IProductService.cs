using Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> ObtenerTodos();
        Task<ProductDto> Agregar(ProductDto modeloDto);
        Task Actualizar(ProductDto modeloDto);
        Task Remover(int id);
    }
}
