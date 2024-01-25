namespace PublishCore.Publish.Application.Dtos.ServicioBeneficio.Response
{
    public class ServicioBeneficioResponseDto
    {
        public int Id { get; set; }
        public string? Titulo { get; set; }
        public string? Descripcion { get; set; }
        public string? Imagen { get; set; }
        public DateTime FechaCreacionAuditoria { get; set; }
        public int EmpresaId { get; set; }
        public int Estado { get; set; }
        public int Programacion { get; set; }
        public DateTime FechaProgramacion { get; set; }
        public string? Empresa {  get; set; }
        public string? EstadoServicioBeneficio { get; set; }
        public string? ProgramacionServicioBeneficio { get; set; }
    }
}