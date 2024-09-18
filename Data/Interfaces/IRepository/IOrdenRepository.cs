using Models.Entities;

namespace Data.Interfaces.IRepository
{
    public interface IOrdenRepository : IRepositoryGenerico<Orden>
    {
        void Actualizar(Orden orden);
    }
}
