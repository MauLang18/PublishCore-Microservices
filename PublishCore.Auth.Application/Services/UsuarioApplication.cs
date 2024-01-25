using AutoMapper;
using Newtonsoft.Json;
using PublishCore.Auth.Application.Commons.Bases;
using PublishCore.Auth.Application.Dtos.Usuario.Request;
using PublishCore.Auth.Application.Dtos.Usuario.Response;
using PublishCore.Auth.Application.Interfaces;
using PublishCore.Auth.Application.Validators.Usuario;
using PublishCore.Auth.Domain.Entities;
using PublishCore.Auth.Infrastructure.Commons.Bases.Request;
using PublishCore.Auth.Infrastructure.Commons.Bases.Response;
using PublishCore.Auth.Infrastructure.Persistences.Interfaces;
using PublishCore.Auth.Utilities.Static;
using WatchDog;
using BC = BCrypt.Net.BCrypt;

namespace PublishCore.Auth.Application.Services
{
    public class UsuarioApplication : IUsuarioApplication
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UsuarioValidator _validator;
        private readonly IProducerApplication _producer;

        public UsuarioApplication(IUnitOfWork unitOfWork, IMapper mapper, UsuarioValidator validator, IProducerApplication producer)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _validator = validator;
            _producer = producer;
        }

        public async Task<BaseResponse<BaseEntityResponse<UsuarioResponseDto>>> ListUsuarios(BaseFiltersRequest filters)
        {
            var response = new BaseResponse<BaseEntityResponse<UsuarioResponseDto>>();
            try
            {
                var usuarios = await _unitOfWork.Usuario.ListUsuarios(filters);

                if (usuarios is not null)
                {
                    response.IsSuccess = true;
                    response.Data = _mapper.Map<BaseEntityResponse<UsuarioResponseDto>>(usuarios);
                    response.Message = ReplyMessage.MESSAGE_QUERY;
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_EXCEPTION;
                WatchLogger.Log(ex.Message);
            }

            return response;
        }

        public async Task<BaseResponse<UsuarioResponseDto>> UsuarioById(int id)
        {
            var response = new BaseResponse<UsuarioResponseDto>();
            try
            {
                var usuarios = await _unitOfWork.Usuario.GetByIdAsync(id);

                if (usuarios is not null)
                {
                    response.IsSuccess = true;
                    response.Data = _mapper.Map<UsuarioResponseDto>(usuarios);
                    response.Message = ReplyMessage.MESSAGE_QUERY;
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_EXCEPTION;
                WatchLogger.Log(ex.Message);
            }

            return response;
        }

        public async Task<BaseResponse<bool>> RegisterUsuario(UsuarioRequestDto requestDto)
        {
            var response = new BaseResponse<bool>();
            try
            {
                var validationResult = await _validator.ValidateAsync(requestDto);

                if (!validationResult.IsValid)
                {
                    response.IsSuccess = false;
                    response.Message = ReplyMessage.MESSAGE_VALIDATE;
                    response.Errors = validationResult.Errors;
                    return response;
                }

                var account = _mapper.Map<TbUsuario>(requestDto);
                account.Pass = BC.HashPassword(account.Pass);

                response.Data = await _unitOfWork.Usuario.RegisterAsync(account);

                if (response.Data)
                {
                    await _producer.ProduceAsync("usuarioRegistrado", JsonConvert.SerializeObject(account));
                    response.IsSuccess = true;
                    response.Message = ReplyMessage.MESSAGE_SAVE;
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = ReplyMessage.MESSAGE_FAILED;
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_EXCEPTION;
                WatchLogger.Log(ex.Message);
            }

            return response;
        }

        public async Task<BaseResponse<bool>> EditUsuario(int id, UsuarioRequestDto requestDto)
        {
            var response = new BaseResponse<bool>();
            try
            {
                var usuarioEdit = await UsuarioById(id);

                if (usuarioEdit.Data is null)
                {
                    response.IsSuccess = false;
                    response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
                    return response;
                }

                var usuario = _mapper.Map<TbUsuario>(requestDto);
                usuario.Id = id;

                if (requestDto.Pass is not null)
                {
                    usuario.Pass = BC.HashPassword(usuario.Pass);
                }
                else
                {
                    usuario.Pass = usuarioEdit.Data.Pass!;
                }

                response.Data = await _unitOfWork.Usuario.EditAsync(usuario);

                if (response.Data)
                {
                    await _producer.ProduceAsync("usuarioActualizado", JsonConvert.SerializeObject(usuario));
                    response.IsSuccess = true;
                    response.Message = ReplyMessage.MESSAGE_UPDATE;
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = ReplyMessage.MESSAGE_FAILED;
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_EXCEPTION;
                Console.WriteLine(ex.ToString());
                WatchLogger.Log(ex.Message);
            }

            return response;
        }

        public async Task<BaseResponse<bool>> RemoveUsuario(int id)
        {
            var response = new BaseResponse<bool>();
            try
            {
                var usuario = await UsuarioById(id);

                if (usuario.Data is null)
                {
                    response.IsSuccess = false;
                    response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
                    return response;
                }

                response.Data = await _unitOfWork.Usuario.RemoveAsync(id);

                if (response.Data)
                {
                    await _producer.ProduceAsync("usuarioEliminado", JsonConvert.SerializeObject(usuario));
                    response.IsSuccess = true;
                    response.Message = ReplyMessage.MESSAGE_DELETE;
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = ReplyMessage.MESSAGE_FAILED;
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_EXCEPTION;
                WatchLogger.Log(ex.Message);
            }

            return response;
        }
    }
}