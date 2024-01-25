using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PublishCore.Auth.Domain.Entities;

namespace PublishCore.Auth.Infrastructure.Persistences.Contexts.Configurations
{
    public class TbPoliticaConfiguration : IEntityTypeConfiguration<TbPolitica>
    {
        public void Configure(EntityTypeBuilder<TbPolitica> builder)
        {
            builder.HasKey(e => e.Id);
            builder.ToTable("TB_POLITICA", "PUBLISCORE");

            builder.Property(e => e.Descripcion).IsUnicode(false);
            builder.Property(e => e.Id).ValueGeneratedOnAdd();
            builder.Property(e => e.Titulo)
                .HasMaxLength(100)
                .IsUnicode(false);
        }
    }
}