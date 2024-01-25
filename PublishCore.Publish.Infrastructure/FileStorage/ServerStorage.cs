using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace PublishCore.Publish.Infrastructure.FileStorage
{
    public class ServerStorage : IServerStorage
    {
        private readonly string _connectionString;

        public ServerStorage(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("RutaServer")!;
        }

        public async Task<string> SaveFile(IFormFile file)
        {
            string ruta = Path.Combine(_connectionString, file.FileName);
            try
            {
                using (FileStream newFile = System.IO.File.Create(ruta))
                {
                    await file.CopyToAsync(newFile);
                    newFile.Flush();
                }
            }
            catch (Exception)
            {

            }

            string rutaRelativa = Path.Combine("https://tranquiexpress.com/imagenes", file.FileName);

            return rutaRelativa;
        }
    }
}