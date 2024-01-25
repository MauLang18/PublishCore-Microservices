using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PublishCore.Auth.Domain.Entities;

namespace PublishCore.Auth.Infrastructure.Persistences.Contexts.Configurations
{
    public class TbEmpresaConfiguration : IEntityTypeConfiguration<TbEmpresa>
    {
        public void Configure(EntityTypeBuilder<TbEmpresa> builder)
        {
            builder.HasKey(e => e.Id);
            builder.ToTable("TB_EMPRESA", "PUBLISHCORE");

            builder.Property(e => e.Id).ValueGeneratedOnAdd();
            builder.Property(e => e.Empresa)
                .HasMaxLength(255)
                .IsUnicode(false);
        }
    }
}