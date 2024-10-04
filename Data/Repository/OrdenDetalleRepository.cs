using Data.Interfaces.IRepository;
using Models.Entities;

namespace Data.Repository
{
    public class OrdenDetalleRepository : Repository<OrdenDetalle>, IOrdenDetalleRepository
    {
        private readonly ApplicationDbContext _db;

        public OrdenDetalleRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Actualizar(OrdenDetalle ordenDetalle)
        {
            var ordenDetalleDb = _db.Ordenes.FirstOrDefault(c => c.Id == ordenDetalle.Id);
            if (ordenDetalleDb != null)
            {
                _db.Update(ordenDetalle);
            }
        }
    }
}
