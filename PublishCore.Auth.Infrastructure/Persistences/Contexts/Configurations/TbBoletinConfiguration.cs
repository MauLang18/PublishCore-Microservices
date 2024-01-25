using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PublishCore.Auth.Domain.Entities;

namespace PublishCore.Auth.Infrastructure.Persistences.Contexts.Configurations
{
    public class TbBoletinConfiguration : IEntityTypeConfiguration<TbBoletin>
    {
        public void Configure(EntityTypeBuilder<TbBoletin> builder)
        {
            builder.HasKey(e => e.Id);
            builder.ToTable("TB_BOLETIN", "PUBLISHCORE");

            builder.HasOne(d => d.EmpresaNavigation)
                .WithMany(p => p.TbBoletins)
                .HasForeignKey(d => d.EmpresaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EMPRESA_BOLETIN");

            builder.Property(e => e.Id).ValueGeneratedOnAdd();
            builder.Property(e => e.Imagen).IsUnicode(false);
            builder.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);
        }
    }
}