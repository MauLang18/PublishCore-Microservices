namespace PublishCore.Auth.Application.Dtos.Usuario.Response
{
    public class UsuarioResponseDto
    {
        public int Id { get; set; }
        public string? Usuario { get; set; }
        public string? Pass { get; set; }
        public DateTime FechaCreacionAuditoria { get; set; }
        public int Estado { get; set; }
        public string? EstadoUsuario { get; set; }
    }
}