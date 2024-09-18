using Models.DTOs;

namespace BLL.Services.Interfaces
{
    public interface IOrdenService
    {
        Task<IEnumerable<OrdenDto>> ObtenerTodos();
        Task<OrdenDto> Agregar(OrdenDto modeloDto);
        Task Actualizar(OrdenDto modeloDto);
        Task Remover(int id);
    }
}