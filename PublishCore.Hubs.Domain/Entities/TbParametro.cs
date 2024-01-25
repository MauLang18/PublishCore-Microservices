namespace PublishCore.Hubs.Domain.Entities;

public partial class TbParametro : BaseEntity
{
    public string Parametro { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    public string Valor { get; set; } = null!;

    public int EmpresaId { get; set; }

    public virtual TbEmpresa EmpresaNavigation { get; set; } = null!;
}