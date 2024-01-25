namespace PublishCore.Publish.Application.Dtos.Boletin.Response
{
    public class BoletinResponseDto
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Imagen { get; set; }
        public DateTime FechaCreacionAuditoria { get; set; }
        public int EmpresaId { get; set; }
        public int Estado { get; set; }
        public int Programacion { get; set; }
        public DateTime FechaProgramacion { get; set; }
        public string? Empresa {  get; set; }
        public string? EstadoBoletin { get; set; }
        public string? ProgramacionBoletin { get; set; }
    }
}