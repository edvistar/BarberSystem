using Data.Interfaces.IRepository;
using Models.Entities;

namespace Data.Repository
{
    public class ServiceRepository : Repository<Service>, IServiceRepository
    {
        private readonly ApplicationDbContext _db;

        public ServiceRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Actualizar(Service service)
        {
            var serviceDb = _db.Services.FirstOrDefault(s => s.Id == service.Id);
            if (serviceDb != null) 
            {
                serviceDb.Name = service.Name;
                serviceDb.Price = service.Price;
                serviceDb.Description = service.Description;
            }
        }
    }
}
