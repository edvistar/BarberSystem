using Data.Interfaces.IRepository;
using Models.DTOs;
using Models.Entities;

namespace Data.Repository
{
    public class UsuarioRepository : Repository<UsuarioAplicacion>, IUsuarioRepository
    {
        private readonly ApplicationDbContext _db;

        public UsuarioRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }


        public void Actualizar(UsuarioAplicacion usuario)
        {
            var usuarioDb = _db.UsuarioAplicacion.FirstOrDefault(s => s.Id == usuario.Id);
            if (usuarioDb != null)
            {
                usuarioDb.Nombres = usuario.Nombres;
            }
        }
    }
}
