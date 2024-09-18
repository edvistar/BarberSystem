using Models.Entities;

namespace Data.Interfaces.IRepository
{
    public interface IServiceRepository : IRepositoryGenerico<Service>
    {
        void Actualizar(Service service);
    }
}
