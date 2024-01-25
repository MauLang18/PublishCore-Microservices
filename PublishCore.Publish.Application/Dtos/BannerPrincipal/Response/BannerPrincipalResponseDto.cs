namespace PublishCore.Application.Dtos.BannerPrincipal.Response
{
    public class BannerPrincipalResponseDto
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Imagen { get; set; }
        public DateTime FechaCreacionAuditoria { get; set; }
        public int EmpresaId { get; set; }
        public int Estado { get; set; }
        public int Programacion { get; set; }
        public DateTime FechaProgramacion { get; set; }
        public string? Empresa { get; set; }
        public string? EstadoBannerPrincipal { get; set; }
        public string? ProgramacionBannerPrincipal { get; set; }
    }
}