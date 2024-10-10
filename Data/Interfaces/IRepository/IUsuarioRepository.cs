using Models.Entities;

namespace Data.Interfaces.IRepository
{
    public interface IUsuarioRepository : IRepositoryGenerico<UsuarioAplicacion>
    {
        void Actualizar(UsuarioAplicacion usuario);
    }
}
