namespace PublishCore.Auth.Domain.Entities;

public partial class TbBoletin : BaseEntity
{
    public string Nombre { get; set; } = null!;

    public string Imagen { get; set; } = null!;

    public int EmpresaId { get; set; }

    public int? Programacion { get; set; }

    public DateTime? FechaProgramacion { get; set; }

    public virtual TbEmpresa EmpresaNavigation { get; set; } = null!;
}