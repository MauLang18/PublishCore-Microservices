using AutoMapper;
using Newtonsoft.Json;
using PublishCore.Publish.Application.Commons.Bases;
using PublishCore.Publish.Application.Dtos.Boletin.Request;
using PublishCore.Publish.Application.Dtos.Boletin.Response;
using PublishCore.Publish.Application.Interfaces;
using PublishCore.Publish.Application.Validators.Boletin;
using PublishCore.Publish.Domain.Entities;
using PublishCore.Publish.Infrastructure.Commons.Bases.Request;
using PublishCore.Publish.Infrastructure.Commons.Bases.Response;
using PublishCore.Publish.Infrastructure.Persistences.Interfaces;
using PublishCore.Publish.Utilities.Static;
using WatchDog;

namespace PublishCore.Publish.Application.Services
{
    public class BoletinApplication : IBoletinApplication
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly BoletinValidator _validator;
        private readonly IProducerApplication _producer;

        public BoletinApplication(IUnitOfWork unitOfWork, IMapper mapper, BoletinValidator validator, IProducerApplication producer)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _validator = validator;
            _producer = producer;
        }

        public async Task<BaseResponse<BaseEntityResponse<BoletinResponseDto>>> ListBoletin(BaseFiltersRequest filters)
        {
            var response = new BaseResponse<BaseEntityResponse<BoletinResponseDto>>();
            try
            {
                var boletin = await _unitOfWork.Boletin.ListBoletin(filters);

                if (boletin is not null)
                {
                    response.IsSuccess = true;
                    response.Data = _mapper.Map<BaseEntityResponse<BoletinResponseDto>>(boletin);
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

        public async Task<BaseResponse<BoletinResponseDto>> BoletinById(int id)
        {
            var response = new BaseResponse<BoletinResponseDto>();
            try
            {
                var boletin = await _unitOfWork.Boletin.GetByIdAsync(id);

                if (boletin is not null)
                {
                    response.IsSuccess = true;
                    response.Data = _mapper.Map<BoletinResponseDto>(boletin);
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

        public async Task<BaseResponse<bool>> RegisterBoletin(BoletinRequestDto requestDto)
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

                var boletin = _mapper.Map<TbBoletin>(requestDto);
                boletin.Imagen = await _unitOfWork.Storage.SaveFile(requestDto.Imagen!);

                boletin.Dirigido = "boletinRegistrado";
                await _producer.ProduceAsync("PublishCore", JsonConvert.SerializeObject(boletin));
                response.Data = await _unitOfWork.Boletin.RegisterAsync(boletin);

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

        public async Task<BaseResponse<bool>> EditBoletin(int id, BoletinRequestDto requestDto)
        {
            var response = new BaseResponse<bool>();
            try
            {
                var boletinEdit = await BoletinById(id);

                if (boletinEdit.Data is null)
                {
                    response.IsSuccess = false;
                    response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
                    return response;
                }

                var boletin = _mapper.Map<TbBoletin>(requestDto);
                boletin.Id = id;

                if (requestDto.Imagen is not null)
                {
                    boletin.Imagen = await _unitOfWork.Storage.SaveFile(requestDto.Imagen!);
                }
                else
                {
                    boletin.Imagen = boletinEdit.Data.Imagen!;
                }

                boletin.Dirigido = "boletinActualizado";
                await _producer.ProduceAsync("PublishCore", JsonConvert.SerializeObject(boletin));
                response.Data = await _unitOfWork.Boletin.EditAsync(boletin);

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

        public async Task<BaseResponse<bool>> RemoveBoletin(int id)
        {
            var response = new BaseResponse<bool>();
            try
            {
                var requestDto = await BoletinById(id);

                if (requestDto.Data is null)
                {
                    response.IsSuccess = false;
                    response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
                    return response;
                }

                var boletin = _mapper.Map<TbBoletin>(requestDto);
                boletin.Dirigido = "boletinRegistrado";
                await _producer.ProduceAsync("PublishCore", JsonConvert.SerializeObject(boletin));
                response.Data = await _unitOfWork.Boletin.RemoveAsync(id);

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