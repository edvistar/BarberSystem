using Data.Interfaces.IRepository;
using Models.Entities;

namespace Data.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _db;

        public ProductRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Actualizar(Product product)
        {
            var productDb = _db.Products.FirstOrDefault(c => c.Id == product.Id);
            if (productDb != null)
            {
                productDb.Name = product.Name;
                productDb.SerialNumber = product.SerialNumber;
                productDb.Description = product.Description;
                productDb.Status = product.Status;
                productDb.Offer = product.Offer;
                productDb.Price = product.Price;
                productDb.Cost = product.Cost;
                productDb.ImageUrl = product.ImageUrl;
                productDb.Categoria = product.Categoria;
                productDb.Marca = product.Marca;

            }
        }
    }
}
