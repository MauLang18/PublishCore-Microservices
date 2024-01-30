using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace PublishCore.Hosted.Api.Entities
{
    public partial class TbBannerPrincipal : BaseEntity
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
}