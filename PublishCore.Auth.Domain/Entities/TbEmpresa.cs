using System.ComponentModel.DataAnnotations.Schema;

namespace PublishCore.Auth.Domain.Entities;

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

    [NotMapped]
    public string? Dirigido { get; set; }

    public virtual ICollection<TbBannerPrincipal> TbBannerPrincipals { get; set; }
    public virtual ICollection<TbBoletin> TbBoletins { get; set; }
    public virtual ICollection<TbParametro> TbParametros { get; set; }
    public virtual ICollection<TbServicioBeneficio> TbServicioBeneficios { get; set; }
}