using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Model.Entities;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class ApplicationDbContext : IdentityDbContext<UsuarioAplicacion, RolAplicacion, int, IdentityUserClaim<int>
                                        , RolUsuarioAplicacion, IdentityUserLogin<int>, IdentityRoleClaim<int>
                                        , IdentityUserToken<int>>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<UsuarioAplicacion> UsuarioAplicacion { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Chair> Chair { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Orden> Ordenes { get; set; }
        public DbSet<Especialidad> Especialidades { get; set; }
        public DbSet<ChairServices> ChairServices { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Marca> Marca { get; set; }
        public DbSet<Category> Categories { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
           modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
