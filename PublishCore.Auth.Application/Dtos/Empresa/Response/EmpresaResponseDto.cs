namespace PublishCore.Auth.Application.Dtos.Empresa.Response
{
    public class EmpresaResponseDto
    {
        public int Id { get; set; }
        public string? Empresa { get; set; }
        public DateTime FechaCreacionAuditoria { get; set; }
        public int Estado { get; set; }
        public string? EstadoEmpresa { get; set; }
    }
}