using Models.Entities;

namespace Data.Interfaces.IRepository
{
    public interface ICategoryRepository : IRepositoryGenerico<Category>
    {
        void Actualizar(Category categoria);
    }
}
