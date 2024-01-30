using AutoMapper;
using Confluent.Kafka;
using Newtonsoft.Json;
using PublishCore.Publish.Application.Commons.Bases;
using PublishCore.Publish.Application.Dtos.ServicioBeneficio.Request;
using PublishCore.Publish.Application.Dtos.ServicioBeneficio.Response;
using PublishCore.Publish.Application.Interfaces;
using PublishCore.Publish.Application.Validators.ServicioBeneficio;
using PublishCore.Publish.Domain.Entities;
using PublishCore.Publish.Infrastructure.Commons.Bases.Request;
using PublishCore.Publish.Infrastructure.Commons.Bases.Response;
using PublishCore.Publish.Infrastructure.Persistences.Interfaces;
using PublishCore.Publish.Utilities.Static;
using WatchDog;

namespace PublishCore.Publish.Application.Services
{
    public class ServicioBeneficioApplication : IServicioBeneficioApplication
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ServicioBeneficioValidator _validator;
        private readonly IProducerApplication _producer;

        public ServicioBeneficioApplication(IUnitOfWork unitOfWork, IMapper mapper, ServicioBeneficioValidator validator, IProducerApplication producer)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _validator = validator;
            _producer = producer;
        }

        public async Task<BaseResponse<BaseEntityResponse<ServicioBeneficioResponseDto>>> ListServicioBeneficio(BaseFiltersRequest filters)
        {
            var response = new BaseResponse<BaseEntityResponse<ServicioBeneficioResponseDto>>();
            try
            {
                var servicioBeneficio = await _unitOfWork.ServicioBeneficio.ListServicioBeneficio(filters);

                if (servicioBeneficio is not null)
                {
                    response.IsSuccess = true;
                    response.Data = _mapper.Map<BaseEntityResponse<ServicioBeneficioResponseDto>>(servicioBeneficio);
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

        public async Task<BaseResponse<ServicioBeneficioResponseDto>> ServivioBeneficioById(int id)
        {
            var response = new BaseResponse<ServicioBeneficioResponseDto>();
            try
            {
                var servicioBeneficio = await _unitOfWork.ServicioBeneficio.GetByIdAsync(id);

                if (servicioBeneficio is not null)
                {
                    response.IsSuccess = true;
                    response.Data = _mapper.Map<ServicioBeneficioResponseDto>(servicioBeneficio);
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

        public async Task<BaseResponse<bool>> RegisterServicioBeneficio(ServicioBeneficioRequestDto requestDto)
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

                var servicioBeneficio = _mapper.Map<TbServicioBeneficio>(requestDto);
                servicioBeneficio.Imagen = await _unitOfWork.Storage.SaveFile(requestDto.Imagen!);

                servicioBeneficio.Dirigido = "servicioBeneficioRegistrado";
                await _producer.ProduceAsync("PublishCore", JsonConvert.SerializeObject(servicioBeneficio));
                response.Data = await _unitOfWork.ServicioBeneficio.RegisterAsync(servicioBeneficio);

                if (response.Data)
                {
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

        public async Task<BaseResponse<bool>> EditServicioBeneficio(int id, ServicioBeneficioRequestDto requestDto)
        {
            var response = new BaseResponse<bool>();
            try
            {
                var servicioBeneficioEdit = await ServivioBeneficioById(id);

                if (servicioBeneficioEdit.Data is null)
                {
                    response.IsSuccess = false;
                    response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
                    return response;
                }

                var servicioBeneficio = _mapper.Map<TbServicioBeneficio>(requestDto);
                servicioBeneficio.Id = id;

                if (requestDto.Imagen is not null)
                {
                    servicioBeneficio.Imagen = await _unitOfWork.Storage.SaveFile(requestDto.Imagen!);
                }
                else
                {
                    servicioBeneficio.Imagen = servicioBeneficioEdit.Data.Imagen!;
                }

                servicioBeneficio.Dirigido = "servicioBeneficioActualizado";
                await _producer.ProduceAsync("PublishCore", JsonConvert.SerializeObject(servicioBeneficio));
                response.Data = await _unitOfWork.ServicioBeneficio.EditAsync(servicioBeneficio);

                if (response.Data)
                {
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

        public async Task<BaseResponse<bool>> RemoveServicioBeneficio(int id)
        {
            var response = new BaseResponse<bool>();
            try
            {
                var requestDto = await ServivioBeneficioById(id);

                if (requestDto.Data is null)
                {
                    response.IsSuccess = false;
                    response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
                    return response;
                }
                var servicioBeneficio = _mapper.Map<TbServicioBeneficio>(requestDto);
                servicioBeneficio.Dirigido = "servicioBeneficioEliminado";
                await _producer.ProduceAsync("PublishCore", JsonConvert.SerializeObject(servicioBeneficio));
                response.Data = await _unitOfWork.ServicioBeneficio.RemoveAsync(id);

                if (response.Data)
                {
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