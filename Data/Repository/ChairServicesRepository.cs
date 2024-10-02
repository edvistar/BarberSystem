using Data.Interfaces.IRepository;
using Models.Entities;

namespace Data.Repository
{
    public class ChairServicesRepository : Repository<ChairServices>, IChairServicesRepository
    {
        private readonly ApplicationDbContext _db;

        public ChairServicesRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
    }
}
