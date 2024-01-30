using System.ComponentModel.DataAnnotations.Schema;

namespace PublishCore.Auth.Domain.Entities;

public partial class TbUsuario : BaseEntity
{
    public string Usuario { get; set; } = null!;

    public string Pass { get; set; } = null!;

    [NotMapped]
    public string? Dirigido { get; set; }
}