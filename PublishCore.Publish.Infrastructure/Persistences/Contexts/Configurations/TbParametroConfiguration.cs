using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PublishCore.Publish.Domain.Entities;

namespace PublishCore.Publish.Infrastructure.Persistences.Contexts.Configurations
{
    public class TbParametroConfiguration : IEntityTypeConfiguration<TbParametro>
    {
        public void Configure(EntityTypeBuilder<TbParametro> builder)
        {
            builder.HasKey(e => e.Id);
            builder.ToTable("TB_PARAMETRO","PUBLISHCORE");

            builder.HasOne(d => d.EmpresaNavigation)
                   .WithMany(p => p.TbParametros)
                   .HasForeignKey(d => d.EmpresaId)
                   .OnDelete(DeleteBehavior.ClientSetNull)
                   .HasConstraintName("FK_EMPRESA_PARAMETRO");

            builder.Property(e => e.Descripcion).IsUnicode(false);
            builder.Property(e => e.Id).ValueGeneratedOnAdd();
            builder.Property(e => e.Parametro)
                .HasMaxLength(100)
                .IsUnicode(false);
            builder.Property(e => e.Valor).IsUnicode(false);
        }
    }
}