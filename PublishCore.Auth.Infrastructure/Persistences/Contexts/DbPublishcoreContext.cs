using Microsoft.EntityFrameworkCore;
using PublishCore.Auth.Domain.Entities;
using System.Reflection;

namespace PublishCore.Auth.Infrastructure.Persistences.Contexts;

public partial class DbPublishcoreContext : DbContext
{
    public DbPublishcoreContext()
    {
    }

    public DbPublishcoreContext(DbContextOptions<DbPublishcoreContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TbBannerPrincipal> TbBannerPrincipals { get; set; }

    public virtual DbSet<TbBoletin> TbBoletins { get; set; }

    public virtual DbSet<TbEmpresa> TbEmpresas { get; set; }

    public virtual DbSet<TbParametro> TbParametros { get; set; }

    public virtual DbSet<TbPolitica> TbPoliticas { get; set; }

    public virtual DbSet<TbPreguntaFrecuente> TbPreguntaFrecuentes { get; set; }

    public virtual DbSet<TbServicioBeneficio> TbServicioBeneficios { get; set; }

    public virtual DbSet<TbUsuario> TbUsuarios { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasAnnotation("Relational.Collaction", "Modern_Spanish_CI_AS");

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}