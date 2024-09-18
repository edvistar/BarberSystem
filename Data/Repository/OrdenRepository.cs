using Data.Interfaces.IRepository;
using Models.Entities;

namespace Data.Repository
{
    public class OrdenRepository : Repository<Orden>, IOrdenRepository
    {
        private readonly ApplicationDbContext _db;

        public OrdenRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Actualizar(Orden orden)
        {
            var ordenDb = _db.Ordenes.FirstOrDefault(c => c.Id == orden.Id);
            if (ordenDb != null)
            {
                ordenDb.Numero = orden.Numero;
                ordenDb.Servicios = orden.Servicios;
                ordenDb.NombreCliente = orden.NombreCliente;
                ordenDb.UsuarioAtiende = orden.UsuarioAtiende;
            }
        }
    }
}
