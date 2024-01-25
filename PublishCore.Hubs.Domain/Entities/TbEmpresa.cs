namespace PublishCore.Hubs.Domain.Entities;

public partial class TbEmpresa : BaseEntity
{
    public TbEmpresa()
    {
        TbBannerPrincipals = new HashSet<TbBannerPrincipal>();
        TbBoletins = new HashSet<TbBoletin>();
        TbParametros = new HashSet<TbParametro>();
        TbServicioBeneficios = new HashSet<TbServicioBeneficio>();
    }

    public string Empresa { get; set; } = null!;

    public int EmpresaId { get; set; }

    public virtual ICollection<TbBannerPrincipal> TbBannerPrincipals { get; set; }
    public virtual ICollection<TbBoletin> TbBoletins { get; set; }
    public virtual ICollection<TbParametro> TbParametros { get; set; }
    public virtual ICollection<TbServicioBeneficio> TbServicioBeneficios { get; set; }
}