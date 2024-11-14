using Data.Interfaces.IRepository;
using Models.Entities;

namespace Data.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly ApplicationDbContext _db;

        public CategoryRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Actualizar(Category categoria)
        {
            var categoriaDb = _db.Categories.FirstOrDefault(c => c.Id == categoria.Id);
            if (categoriaDb != null)
            {
                categoriaDb.Name = categoria.Name;
                categoriaDb.Description = categoria.Description;
                categoriaDb.Status = categoria.Status;
            }
        }
    }
}
