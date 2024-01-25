using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PublishCore.Publish.Domain.Entities;

namespace PublishCore.Publish.Infrastructure.Persistences.Contexts.Configurations
{
    public class TbServicioBeneficioConfiguration : IEntityTypeConfiguration<TbServicioBeneficio>
    {
        public void Configure(EntityTypeBuilder<TbServicioBeneficio> builder)
        {
            builder.HasKey(e => e.Id);
            builder.ToTable("TB_SERVICIO_BENEFICIO", "PUBLISHCORE");

            builder.HasOne(d => d.EmpresaNavigation)
                   .WithMany(p => p.TbServicioBeneficios)
                   .HasForeignKey(d => d.EmpresaId)
                   .OnDelete(DeleteBehavior.ClientSetNull)
                   .HasConstraintName("FK_EMPRESA_SERVICIO_BENEFICIO");

            builder.Property(e => e.Descripcion).IsUnicode(false);
            builder.Property(e => e.Id).ValueGeneratedOnAdd();
            builder.Property(e => e.Imagen).IsUnicode(false);
            builder.Property(e => e.Titulo)
                .HasMaxLength(100)
                .IsUnicode(false);
        }
    }
}