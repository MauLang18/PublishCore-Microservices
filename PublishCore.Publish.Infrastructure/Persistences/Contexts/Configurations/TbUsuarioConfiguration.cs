using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PublishCore.Publish.Domain.Entities;

namespace PublishCore.Publish.Infrastructure.Persistences.Contexts.Configurations
{
    public class TbUsuarioConfiguration : IEntityTypeConfiguration<TbUsuario>
    {
        public void Configure(EntityTypeBuilder<TbUsuario> builder)
        {
            builder.HasKey(e => e.Id);
            builder.ToTable("TB_USUARIO", "PUBLISHCORE");

            builder.Property(e => e.Id).ValueGeneratedOnAdd();
            builder.Property(e => e.Pass).IsUnicode(false);
            builder.Property(e => e.Usuario)
                .HasMaxLength(100)
                .IsUnicode(false);
        }
    }
}