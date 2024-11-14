using Models.Entities;

namespace Data.Interfaces.IRepository
{
    public interface IProductRepository : IRepositoryGenerico<Product>
    {
        void Actualizar(Product product);
    }
}
