namespace PublishCore.Publish.Application.Dtos.Parametro.Response
{
    public class ParametroResponseDto
    {
        public int Id { get; set; }
        public string? Parametro { get; set; }
        public string? Descripcion { get; set; }
        public string? Valor { get; set; }
        public DateTime FechaCreacionAuditoria { get; set; }
        public int EmpresaId { get; set; }
        public int Estado { get; set; }
        public string? Empresa {  get; set; }
        public string? EstadoParametro { get; set; }
    }
}