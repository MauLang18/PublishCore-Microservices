using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace PublishCore.Hosted.Api.Entities
{
    public partial class TbPreguntaFrecuente : BaseEntity
    {
        public string Titulo { get; set; } = null!;

        public string Descripcion { get; set; } = null!;

        public int EmpresaId { get; set; }

        public int? Programacion { get; set; }

        public DateTime? FechaProgramacion { get; set; }
    }
}