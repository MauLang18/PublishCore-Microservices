using Microsoft.AspNetCore.Http;

namespace PublishCore.Publish.Infrastructure.FileStorage
{
    public interface IServerStorage
    {
        Task<string> SaveFile(IFormFile file);
    }
}