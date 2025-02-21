using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace N5_Challenge.Models.Entities
{
    public class Permiso
    {
        public int Id { get; set; }
        public Guid IdGuid { get; set; }
        public int IdEmpleado { get; set; }
        public virtual Empleado Empleado { get; set; }
        public int IdTipoPermiso { get; set; }
        public virtual TipoPermiso TipoPermiso { get; set; }
        public DateTime FechaSolicitud { get; set; }
        public bool Estado { get; set; }
        public DateTime? FechaAprobacion { get; set; }
        public DateTime? FechaVencimiento { get; set; }
    }

    public class PermisoMap : IEntityTypeConfiguration<Permiso>
    {
        public void Configure(EntityTypeBuilder<Permiso> builder)
        {
            builder.ToTable("Permiso");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.IdGuid).HasColumnName("IdGuid");
            builder.Property(x => x.IdEmpleado).HasColumnName("IdEmpleado");
            builder.Property(x => x.IdTipoPermiso).HasColumnName("IdTipoPermiso");
            builder.Property(x => x.FechaSolicitud).HasColumnName("FechaSolicitud");
            builder.Property(x => x.FechaAprobacion).HasColumnName("FechaAprobacion");
            builder.Property(x => x.FechaVencimiento).HasColumnName("FechaVencimiento");

            builder.HasOne(x => x.TipoPermiso).WithMany().HasForeignKey(x => x.IdTipoPermiso);
            builder.HasOne(x => x.Empleado).WithMany().HasForeignKey(x => x.IdEmpleado);
        }
    }
}
