namespace PublishCore.Auth.Domain.Entities;

public partial class TbPolitica : BaseEntity
{
    public string Titulo { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    public int EmpresaId { get; set; }

    public int? Programacion { get; set; }

    public DateTime? FechaProgramacion { get; set; }
}