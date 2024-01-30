using System.ComponentModel.DataAnnotations.Schema;

namespace PublishCore.Publish.Domain.Entities;

public partial class TbBoletin : BaseEntity
{
    public string Nombre { get; set; } = null!;

    public string Imagen { get; set; } = null!;

    public int EmpresaId { get; set; }

    public int? Programacion { get; set; }

    public DateTime? FechaProgramacion { get; set; }

    [NotMapped]
    public string? Dirigido { get; set; }

    public virtual TbEmpresa EmpresaNavigation { get; set; } = null!;
}