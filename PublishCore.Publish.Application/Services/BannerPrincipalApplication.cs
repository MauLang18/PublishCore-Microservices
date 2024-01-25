using AutoMapper;
using Newtonsoft.Json;
using PublishCore.Application.Dtos.BannerPrincipal.Response;
using PublishCore.Publish.Application.Commons.Bases;
using PublishCore.Publish.Application.Dtos.BannerPrincipal.Request;
using PublishCore.Publish.Application.Interfaces;
using PublishCore.Publish.Application.Validators.BannerPrincipal;
using PublishCore.Publish.Domain.Entities;
using PublishCore.Publish.Infrastructure.Commons.Bases.Request;
using PublishCore.Publish.Infrastructure.Commons.Bases.Response;
using PublishCore.Publish.Infrastructure.Persistences.Interfaces;
using PublishCore.Publish.Utilities.Static;
using WatchDog;

namespace PublishCore.Publish.Application.Services
{
    public class BannerPrincipalApplication : IBannerPrincipalApplication
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly BannerPrincipalValidator _validator;
        private readonly IProducerApplication _producer;

        public BannerPrincipalApplication(IUnitOfWork unitOfWork, IMapper mapper, BannerPrincipalValidator validator, IProducerApplication producer)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _validator = validator;
            _producer = producer;
        }

        public async Task<BaseResponse<BaseEntityResponse<BannerPrincipalResponseDto>>> ListBannerPrincipal(BaseFiltersRequest filters)
        {
            var response = new BaseResponse<BaseEntityResponse<BannerPrincipalResponseDto>>();
            try
            {
                var bannerPrincipal = await _unitOfWork.BannerPrincipal.ListBannerPrincipal(filters);

                if (bannerPrincipal is not null)
                {
                    response.IsSuccess = true;
                    response.Data = _mapper.Map<BaseEntityResponse<BannerPrincipalResponseDto>>(bannerPrincipal);
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

        public async Task<BaseResponse<BannerPrincipalResponseDto>> BannerPrincipalById(int id)
        {
            var response = new BaseResponse<BannerPrincipalResponseDto>();
            try
            {
                var bannerPrincipal = await _unitOfWork.BannerPrincipal.GetByIdAsync(id);

                if (bannerPrincipal is not null)
                {
                    response.IsSuccess = true;
                    response.Data = _mapper.Map<BannerPrincipalResponseDto>(bannerPrincipal);
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

        public async Task<BaseResponse<bool>> RegisterBannerPrincipal(BannerPrincipalRequestDto request)
        {
            var response = new BaseResponse<bool>();
            try
            {
                var validationResult = await _validator.ValidateAsync(request);

                if (!validationResult.IsValid)
                {
                    response.IsSuccess = false;
                    response.Message = ReplyMessage.MESSAGE_VALIDATE;
                    response.Errors = validationResult.Errors;
                    return response;
                }

                var bannerPrincipal = _mapper.Map<TbBannerPrincipal>(request);
                bannerPrincipal.Imagen = await _unitOfWork.Storage.SaveFile(request.Imagen!);
                response.Data = await _unitOfWork.BannerPrincipal.RegisterAsync(bannerPrincipal);

                if (response.Data)
                {
                    await _producer.ProduceAsync("bannerRegistrado", JsonConvert.SerializeObject(bannerPrincipal));
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

        public async Task<BaseResponse<bool>> EditBannerPrincipal(int id, BannerPrincipalRequestDto request)
        {
            var response = new BaseResponse<bool>();
            try
            {
                var bannerPrincipalEdit = await BannerPrincipalById(id);

                if (bannerPrincipalEdit.Data is null)
                {
                    response.IsSuccess = false;
                    response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
                    return response;
                }

                var bannerPrincipal = _mapper.Map<TbBannerPrincipal>(request);
                bannerPrincipal.Id = id;

                if (request.Imagen is not null)
                {
                    bannerPrincipal.Imagen = await _unitOfWork.Storage.SaveFile(request.Imagen!);
                }

                else
                {
                    bannerPrincipal.Imagen = bannerPrincipalEdit.Data.Imagen!;
                }

                response.Data = await _unitOfWork.BannerPrincipal.EditAsync(bannerPrincipal);

                if (response.Data)
                {
                    await _producer.ProduceAsync("bannerActualizado", JsonConvert.SerializeObject(bannerPrincipal));
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

        public async Task<BaseResponse<bool>> RemoveBannerPrincipal(int id)
        {
            var response = new BaseResponse<bool>();
            try
            {
                var bannerPrincipal = await BannerPrincipalById(id);

                if (bannerPrincipal.Data is null)
                {
                    response.IsSuccess = false;
                    response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
                    return response;
                }

                response.Data = await _unitOfWork.BannerPrincipal.RemoveAsync(id);

                if (response.Data)
                {
                    await _producer.ProduceAsync("bannerEliminado", JsonConvert.SerializeObject(bannerPrincipal));
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