using PublishCore.Auth.Application.Commons.Bases;
using PublishCore.Auth.Application.Dtos.Usuario.Request;

namespace PublishCore.Auth.Application.Interfaces
{
    public interface IAuthApplication
    {
        Task<BaseResponse<string>> Login(TokenRequestDto requestDto);
    }
}