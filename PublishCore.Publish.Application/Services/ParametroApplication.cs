using AutoMapper;
using Newtonsoft.Json;
using PublishCore.Publish.Application.Commons.Bases;
using PublishCore.Publish.Application.Dtos.Parametro.Request;
using PublishCore.Publish.Application.Dtos.Parametro.Response;
using PublishCore.Publish.Application.Interfaces;
using PublishCore.Publish.Application.Validators.Parametro;
using PublishCore.Publish.Domain.Entities;
using PublishCore.Publish.Infrastructure.Commons.Bases.Request;
using PublishCore.Publish.Infrastructure.Commons.Bases.Response;
using PublishCore.Publish.Infrastructure.Persistences.Interfaces;
using PublishCore.Publish.Utilities.Static;
using WatchDog;

namespace PublishCore.Publish.Application.Services
{
    public class ParametroApplication : IParametroApplication
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ParametroValidator _validator;
        private readonly IProducerApplication _producer;

        public ParametroApplication(IUnitOfWork unitOfWork, IMapper mapper, ParametroValidator validator, IProducerApplication producer)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _validator = validator;
            _producer = producer;
        }

        public async Task<BaseResponse<BaseEntityResponse<ParametroResponseDto>>> ListParametros(BaseFiltersRequest filters)
        {
            var response = new BaseResponse<BaseEntityResponse<ParametroResponseDto>>();
            try
            {
                var parametros = await _unitOfWork.Parametro.ListParametro(filters);

                if (parametros is not null)
                {
                    response.IsSuccess = true;
                    response.Data = _mapper.Map<BaseEntityResponse<ParametroResponseDto>>(parametros);
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

        public async Task<BaseResponse<ParametroResponseDto>> ParametroById(int id)
        {
            var response = new BaseResponse<ParametroResponseDto>();
            try
            {
                var parametros = await _unitOfWork.Parametro.GetByIdAsync(id);

                if (parametros is not null)
                {
                    response.IsSuccess = true;
                    response.Data = _mapper.Map<ParametroResponseDto>(parametros);
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

        public async Task<BaseResponse<bool>> RegisterParametro(ParametroRequestDto requestDto)
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

                var parametro = _mapper.Map<TbParametro>(requestDto);
                response.Data = await _unitOfWork.Parametro.RegisterAsync(parametro);

                if (response.Data)
                {
                    await _producer.ProduceAsync("parametroRegistrado", JsonConvert.SerializeObject(parametro));
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

        public async Task<BaseResponse<bool>> EditParametro(int id, ParametroRequestDto requestDto)
        {
            var response = new BaseResponse<bool>();
            try
            {
                var parametroEdit = await ParametroById(id);

                if (parametroEdit.Data is null)
                {
                    response.IsSuccess = false;
                    response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
                    return response;
                }

                var parametro = _mapper.Map<TbParametro>(requestDto);
                parametro.Id = id;
                response.Data = await _unitOfWork.Parametro.EditAsync(parametro);

                if (response.Data)
                {
                    await _producer.ProduceAsync("parametroActualizado", JsonConvert.SerializeObject(parametro));
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
                WatchLogger.Log(ex.Message);
            }

            return response;
        }

        public async Task<BaseResponse<bool>> RemoveParametro(int id)
        {
            var response = new BaseResponse<bool>();
            try
            {
                var parametro = await ParametroById(id);

                if (parametro.Data is null)
                {
                    response.IsSuccess = false;
                    response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
                    return response;
                }

                response.Data = await _unitOfWork.Parametro.RemoveAsync(id);

                if (response.Data)
                {
                    await _producer.ProduceAsync("parametroEliminado", JsonConvert.SerializeObject(parametro));
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