using Models.DTOs;

namespace BLL.Services.Interfaces
{
    public interface IServiceService
    {
        Task<IEnumerable<ServiceDto>> ObtenerTodos();
        Task<ServiceDto> Agregar(ServiceDto modeloDto);
        Task Actualizar(ServiceDto modeloDto);
        Task Remover(int id);
    }
}