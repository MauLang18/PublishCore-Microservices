namespace PublishCore.Publish.Application.Dtos.Parametro.Request
{
    public class ParametroRequestDto
    {
        public string? Parametro { get; set; }
        public string? Descripcion { get; set; }
        public string? Valor { get; set; }
        public int EmpresaId { get; set; }
        public int Estado { get; set; }
    }
}