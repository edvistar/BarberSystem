using Models.Entities;

namespace Data.Interfaces.IRepository
{
    public interface IMarcaRepository : IRepositoryGenerico<Marca>
    {
        void Actualizar(Marca marca);
    }
}
