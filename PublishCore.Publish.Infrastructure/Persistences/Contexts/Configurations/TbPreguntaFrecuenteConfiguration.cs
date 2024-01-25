using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PublishCore.Publish.Domain.Entities;

namespace PublishCore.Publish.Infrastructure.Persistences.Contexts.Configurations
{
    public class TbPreguntaFrecuenteConfiguration : IEntityTypeConfiguration<TbPreguntaFrecuente>
    {
        public void Configure(EntityTypeBuilder<TbPreguntaFrecuente> builder)
        {
            builder.HasKey(e => e.Id);
            builder.ToTable("TB_PREGUNTA_FRECUENTE", "PUBLISCORE");

            builder.Property(e => e.Descripcion).IsUnicode(false);
            builder.Property(e => e.Id).ValueGeneratedOnAdd();
            builder.Property(e => e.Titulo)
                .HasMaxLength(100)
                .IsUnicode(false);
        }
    }
}