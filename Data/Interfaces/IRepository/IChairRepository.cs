using Models.Entities;

namespace Data.Interfaces.IRepository
{
    public interface IChairRepository : IRepositoryGenerico<Chair>
    {
        void Actualizar(Chair chair);
        void ActualizarEstado(Chair chair);
    }
}
