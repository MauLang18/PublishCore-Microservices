using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PublishCore.Publish.Domain.Entities;

namespace PublishCore.Publish.Infrastructure.Persistences.Contexts.Configurations
{
    public class TbEmpresaConfiguration : IEntityTypeConfiguration<TbEmpresa>
    {
        public void Configure(EntityTypeBuilder<TbEmpresa> builder)
        {
            builder.HasKey(e => e.Id).HasName("PK__TB_EMPRE__3214EC071C80C52A");

            builder.ToTable("TB_EMPRESA", "PUBLISHCORE");

            builder.Property(e => e.Empresa)
                .HasMaxLength(255)
                .IsUnicode(false);
        }
    }
}