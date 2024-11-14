using Data.Interfaces.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class UnitWork : IUnitWork
    {
        private readonly ApplicationDbContext _db;
        public IChairRepository Chair { get; private set; }
        public IServiceRepository Service { get; private set; }
        public IOrdenRepository Orden { get; private set; }
        public IChairServicesRepository ChairServices { get; private set; }
        public IOrdenDetalleRepository OrdenDetalle { get; private set; }
        public IUsuarioRepository Usuario { get; private set; }
        public IProductRepository Product { get; private set; }
        public IMarcaRepository Marca { get; private set; }
        public ICategoryRepository Category { get; private set; }

        public UnitWork(ApplicationDbContext db)
        {
            _db = db;
            Chair = new ChairRepository(db);
            Service = new ServiceRepository(db);
            Orden = new OrdenRepository(db);
            ChairServices = new ChairServicesRepository(db);
            OrdenDetalle= new OrdenDetalleRepository(db);
            Usuario = new UsuarioRepository(db);
            Product = new ProductRepository(db);
            Marca = new MarcaRepository(db);
            Category = new CategoryRepository(db);
        }


        public void Dispose()
        {
            _db.Dispose();
        }

        public async Task Guardar()
        {
          await _db.SaveChangesAsync();
        }
    }
}
