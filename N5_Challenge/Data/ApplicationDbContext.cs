
using Microsoft.EntityFrameworkCore;
using N5_Challenge.Models.Entities;

namespace Entities.DbContexts
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new PermisoMap());
            modelBuilder.ApplyConfiguration(new TipoPermisoMap());
            modelBuilder.ApplyConfiguration(new EmpleadoMap());
        }

        public DbSet<Empleado> Empleado { get; set; }
        public DbSet<TipoPermiso> TipoPermiso { get; set; }
        public DbSet<Permiso> Permiso { get; set; }
    }
}
