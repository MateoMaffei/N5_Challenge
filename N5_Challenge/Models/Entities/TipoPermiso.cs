using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace N5_Challenge.Models.Entities
{
    public class TipoPermiso
    {
        public int Id { get; set; }
        public Guid IdGuid { get; set; }
        public string Descripcion { get; set; } = string.Empty;
    }

    public class TipoPermisoMap : IEntityTypeConfiguration<TipoPermiso>
    {
        public void Configure(EntityTypeBuilder<TipoPermiso> builder)
        {
            builder.ToTable("TipoPermiso");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.IdGuid).HasColumnName("IdGuid");
            builder.Property(x => x.Descripcion).HasColumnName("Descripcion");
        }
    }

}
