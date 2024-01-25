namespace PublishCore.Publish.Domain.Entities;

public partial class TbUsuario : BaseEntity
{
    public string Usuario { get; set; } = null!;

    public string Pass { get; set; } = null!;
}