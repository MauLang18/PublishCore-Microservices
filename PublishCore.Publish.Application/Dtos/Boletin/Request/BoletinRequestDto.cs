using Microsoft.AspNetCore.Http;

namespace PublishCore.Publish.Application.Dtos.Boletin.Request
{
    public class BoletinRequestDto
    {
        public string? Nombre { get; set; }
        public IFormFile? Imagen { get; set; }
        public int EmpresaId { get; set; }
        public int Estado { get; set; }
        public int Programacion { get; set; }
        public DateTime FechaProgramacion { get; set; }
    }
}