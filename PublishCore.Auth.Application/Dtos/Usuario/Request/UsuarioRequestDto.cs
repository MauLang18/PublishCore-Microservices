namespace PublishCore.Auth.Application.Dtos.Usuario.Request
{
    public class UsuarioRequestDto
    {
        public string? Usuario { get; set; }
        public string? Pass { get; set; }
        public int Estado { get; set; }
    }
}