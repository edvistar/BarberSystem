using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Models.Entities;

namespace Data.Inicializador
{
    public class DbInicializador : IDbInicializador
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<UsuarioAplicacion> _userManager;
        private readonly RoleManager<RolAplicacion> _rolManager;

        public DbInicializador(ApplicationDbContext db, UserManager<UsuarioAplicacion> userManager,
            RoleManager<RolAplicacion> rolManager)
        {
            _db = db;
            _userManager = userManager;
            _rolManager = rolManager;
        }

        public async void Inicializar()
        {
            try
            {
                if (_db.Database.GetPendingMigrations().Count() > 0)
                {
                    _db.Database.Migrate();  // Ejecuta las migraciones pendientes
                }
            }
            catch (Exception)
            {

                throw;
            }

            //Datos INiciales
            //Crear Roles
            if (_db.Roles.Any(r => r.Name == "Admin")) return;
             _rolManager.CreateAsync(new RolAplicacion { Name = "Admin" }).GetAwaiter().GetResult();
             _rolManager.CreateAsync(new RolAplicacion { Name = "Manicurista"}).GetAwaiter().GetResult();
             _rolManager.CreateAsync(new RolAplicacion { Name = "Barbero" }).GetAwaiter().GetResult();
             _rolManager.CreateAsync(new RolAplicacion { Name = "Estilista" }).GetAwaiter().GetResult();
            _rolManager.CreateAsync(new RolAplicacion { Name = "Cliente" }).GetAwaiter().GetResult();

            //Crear Usuario Administrador

            var usuario = new UsuarioAplicacion
            {
                UserName = "administrador",
                Email = "victorhoyoscolombia@gmail.com",
                Apellidos = "Hoyos",
                Nombres = "Victor"
            };
             await _userManager.CreateAsync(usuario, "Admin123");
            //Asignar Rol Admin al usuario
            UsuarioAplicacion usuarioAdmin = _db.UsuarioAplicacion.Where(u => u.UserName == "Administrador").FirstOrDefault();
            _userManager.AddToRoleAsync(usuarioAdmin, "Admin").GetAwaiter().GetResult();
        }
    }
}
