using Microsoft.AspNetCore.Http;

namespace PublishCore.Publish.Application.Dtos.ServicioBeneficio.Request
{
    public class ServicioBeneficioRequestDto
    {
        public string? Titulo { get; set; }
        public string? Descripcion { get; set; }
        public IFormFile? Imagen { get; set; }
        public int EmpresaId { get; set; }
        public int Estado { get; set; }
        public int Programacion { get; set; }
        public DateTime FechaProgramacion { get; set; }
    }
}