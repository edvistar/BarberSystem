using Data.Interfaces.IRepository;
using Models.Entities;

namespace Data.Repository
{
    public class ChairRepository : Repository<Chair>, IChairRepository
    {
        private readonly ApplicationDbContext _db;

        public ChairRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Actualizar(Chair chair)
        {
            var chairDb = _db.Chair.FirstOrDefault(c => c.Id == chair.Id);
            if (chairDb != null)
            {
                chairDb.Name = chair.Name;
                chairDb.Numero = chair.Numero;
                chairDb.Logo = chair.Logo;
            }
        }
    }
}
