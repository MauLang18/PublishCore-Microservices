using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PublishCore.Auth.Domain.Entities;

namespace PublishCore.Auth.Infrastructure.Persistences.Contexts.Configurations
{
    public class TbBannerPrincipalConfiguration : IEntityTypeConfiguration<TbBannerPrincipal>
    {
        public void Configure(EntityTypeBuilder<TbBannerPrincipal> builder)
        {
            builder.HasKey(e => e.Id);
            builder.ToTable("TB_BANNER_PRINCIPAL", "PUBLISHCORE");

            builder.HasOne(d => d.EmpresaNavigation)
                .WithMany(p => p.TbBannerPrincipals)
                .HasForeignKey(d => d.EmpresaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EMPRESA_BANNER_PRINCIPAL");

            builder.Property(e => e.Id).ValueGeneratedOnAdd();
            builder.Property(e => e.Imagen).IsUnicode(false);
            builder.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);
        }
    }
}