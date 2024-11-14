using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interfaces.IRepository
{
    public interface IUnitWork : IDisposable
    {
        IChairRepository  Chair {  get; }
        IServiceRepository Service { get; }
        IOrdenRepository Orden { get; }
        IChairServicesRepository ChairServices { get; }
        IOrdenDetalleRepository OrdenDetalle { get; }
        IUsuarioRepository Usuario { get; }
        IProductRepository Product { get; }
        IMarcaRepository Marca { get; }
        ICategoryRepository Category { get; }

        Task Guardar();
    }
}
