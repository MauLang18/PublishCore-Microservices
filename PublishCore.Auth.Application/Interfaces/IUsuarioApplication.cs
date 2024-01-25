using PublishCore.Auth.Application.Commons.Bases;
using PublishCore.Auth.Application.Dtos.Usuario.Request;
using PublishCore.Auth.Application.Dtos.Usuario.Response;
using PublishCore.Auth.Infrastructure.Commons.Bases.Request;
using PublishCore.Auth.Infrastructure.Commons.Bases.Response;

namespace PublishCore.Auth.Application.Interfaces
{
    public interface IUsuarioApplication
    {
        Task<BaseResponse<BaseEntityResponse<UsuarioResponseDto>>> ListUsuarios(BaseFiltersRequest filters);
        Task<BaseResponse<UsuarioResponseDto>> UsuarioById(int id);
        Task<BaseResponse<bool>> RegisterUsuario(UsuarioRequestDto requestDto);
        Task<BaseResponse<bool>> EditUsuario(int id, UsuarioRequestDto requestDto);
        Task<BaseResponse<bool>> RemoveUsuario(int id);
    }
}