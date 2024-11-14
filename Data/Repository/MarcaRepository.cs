using Data.Interfaces.IRepository;
using Models.Entities;

namespace Data.Repository
{
    public class MarcaRepository : Repository<Marca>, IMarcaRepository
    {
        private readonly ApplicationDbContext _db;

        public MarcaRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Actualizar(Marca marca)
        {
            var marcaDb = _db.Marca.FirstOrDefault(c => c.Id == marca.Id);
            if (marcaDb != null)
            {
                marcaDb.Name = marca.Name;
                marcaDb.Description = marca.Description;
                marcaDb.Status = marca.Status;
                marcaDb.ImageUrl = marca.ImageUrl;
            }
        }
    }
}
