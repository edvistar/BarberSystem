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

        public UnitWork(ApplicationDbContext db)
        {
            _db = db;
            Chair = new ChairRepository(db);
            Service = new ServiceRepository(db);
            Orden = new OrdenRepository(db);
            ChairServices = new ChairServicesRepository(db);
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
