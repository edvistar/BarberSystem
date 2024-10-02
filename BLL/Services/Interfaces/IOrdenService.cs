using Models.DTOs;

namespace BLL.Services.Interfaces
{
    public interface IOrdenService
    {
        Task<IEnumerable<OrdenDto>> ObtenerTodos();
        Task<OrdenDto> Agregar(OrdenDto modeloDto);
        Task Actualizar(OrdenDto modeloDto);
        Task Remover(int id);
        Task AddServicesChair(int chairId, int serviceId);
        Task RemoveServiceFromChair(int chairId, int serviceId);
        // Nuevo método para obtener los servicios por silla
        Task<IEnumerable<ServiceDto>> ObtenerServiciosPorSilla(int chairId);
    }
}