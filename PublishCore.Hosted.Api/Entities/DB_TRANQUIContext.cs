using Microsoft.EntityFrameworkCore;

namespace PublishCore.Hosted.Api.Entities
{
    public partial class DB_TRANQUIContext : DbContext
    {
        public DB_TRANQUIContext()
        {
        }

        public DB_TRANQUIContext(DbContextOptions<DB_TRANQUIContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TbBannerPrincipal> TbBannerPrincipals { get; set; }
        public virtual DbSet<TbBoletin> TbBoletins { get; set; }
        public virtual DbSet<TbParametro> TbParametros { get; set; }
        public virtual DbSet<TbPolitica> TbPoliticas { get; set; }
        public virtual DbSet<TbPreguntaFrecuente> TbPreguntaFrecuentes { get; set; }
        public virtual DbSet<TbServicioBeneficio> TbServicioBeneficios { get; set; }
        public virtual DbSet<TbUsuario> TbUsuarios { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=192.168.201.26;database=DB_PUBLISHCORE;user=sa;password=Drainsa1698;Connect Timeout=60;TrustServerCertificate=True;Max Pool Size=50;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Modern_Spanish_CI_AS");

            modelBuilder.Entity<TbBannerPrincipal>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.ToTable("TB_BANNER_PRINCIPAL", "PUBLISHCORE");

                entity.HasOne(d => d.EmpresaNavigation)
                      .WithMany(p => p.TbBannerPrincipals)
                      .HasForeignKey(d => d.EmpresaId)
                      .OnDelete(DeleteBehavior.ClientSetNull)
                      .HasConstraintName("FK_EMPRESA_BANNER_PRINCIPAL");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Imagen)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TbBoletin>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.ToTable("TB_BOLETIN", "PUBLISHCORE");

                entity.HasOne(d => d.EmpresaNavigation)
                      .WithMany(p => p.TbBoletins)
                      .HasForeignKey(d => d.EmpresaId)
                      .OnDelete(DeleteBehavior.ClientSetNull)
                      .HasConstraintName("FK_EMPRESA_BOLETIN");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Imagen)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TbParametro>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.ToTable("TB_PARAMETRO", "PUBLISHCORE");

                entity.HasOne(d => d.EmpresaNavigation)
                      .WithMany(p => p.TbParametros)
                      .HasForeignKey(d => d.EmpresaId)
                      .OnDelete(DeleteBehavior.ClientSetNull)
                      .HasConstraintName("FK_EMPRESA_PARAMETRO");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Parametro)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Valor)
                    .IsRequired()
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TbPolitica>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.ToTable("TB_POLITICA", "PUBLISHCORE");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Titulo)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TbPreguntaFrecuente>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.ToTable("TB_PREGUNTA_FRECUENTE", "PUBLISHCORE");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Titulo)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TbServicioBeneficio>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.ToTable("TB_SERVICIO_BENEFICIO", "PUBLISHCORE");

                entity.HasOne(d => d.EmpresaNavigation)
                      .WithMany(p => p.TbServicioBeneficios)
                      .HasForeignKey(d => d.EmpresaId)
                      .OnDelete(DeleteBehavior.ClientSetNull)
                      .HasConstraintName("FK_EMPRESA_SERVICIO_BENEFICIO");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Imagen)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.Titulo)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TbUsuario>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.ToTable("TB_USUARIO", "PUBLISHCORE");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Pass)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.Usuario)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}