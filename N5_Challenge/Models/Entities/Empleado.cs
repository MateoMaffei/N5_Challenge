using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace N5_Challenge.Models.Entities
{
    public class Empleado
    {
        public int Id { get; set; }
        public Guid IdGuid { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
        public virtual ICollection<Permiso> Permisos { get; set; } = new List<Permiso>();
    }

    public class EmpleadoMap : IEntityTypeConfiguration<Empleado>
    {
        public void Configure(EntityTypeBuilder<Empleado> builder)
        {
            builder.ToTable("Empleado");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.IdGuid).HasColumnName("IdGuid");
            builder.Property(x => x.Nombre).HasColumnName("Nombre");
            builder.Property(x => x.Apellido).HasColumnName("Apellido");
            builder.HasMany(x => x.Permisos).WithOne().HasForeignKey(x => x.Id);
        }
    }
}
