using AutoMapper;
using Confluent.Kafka;
using Newtonsoft.Json;
using PublishCore.Auth.Application.Commons.Bases;
using PublishCore.Auth.Application.Dtos.Empresa.Request;
using PublishCore.Auth.Application.Dtos.Empresa.Response;
using PublishCore.Auth.Application.Interfaces;
using PublishCore.Auth.Domain.Entities;
using PublishCore.Auth.Infrastructure.Commons.Bases.Request;
using PublishCore.Auth.Infrastructure.Commons.Bases.Response;
using PublishCore.Auth.Infrastructure.Persistences.Interfaces;
using PublishCore.Auth.Utilities.Static;
using WatchDog;

namespace PublishCore.Auth.Application.Services
{
    public class EmpresaApplication : IEmpresaApplication
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EmpresaApplication(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseResponse<BaseEntityResponse<EmpresaResponseDto>>> ListEmpresas(BaseFiltersRequest filters)
        {
            var response = new BaseResponse<BaseEntityResponse<EmpresaResponseDto>>();
            try
            {
                var empresas = await _unitOfWork.Empresa.ListEmpresas(filters);

                if (empresas is not null)
                {
                    response.IsSuccess = true;
                    response.Data = _mapper.Map<BaseEntityResponse<EmpresaResponseDto>>(empresas);
                    response.Message = ReplyMessage.MESSAGE_QUERY;
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
                }
            }
            catch (Exception)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_EXCEPTION;
            }

            return response;
        }

        public async Task<BaseResponse<IEnumerable<EmpresaSelectResponseDto>>> ListSelectEmpresa()
        {
            var response = new BaseResponse<IEnumerable<EmpresaSelectResponseDto>>();
            try
            {
                var empresas = await _unitOfWork.Empresa.GetAllAsync();

                if (empresas is not null)
                {
                    response.IsSuccess = true;
                    response.Data = _mapper.Map<IEnumerable<EmpresaSelectResponseDto>>(empresas);
                    response.Message = ReplyMessage.MESSAGE_QUERY;
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
                }
            }
            catch (Exception)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_EXCEPTION;
            }

            return response;
        }

        public async Task<BaseResponse<EmpresaResponseDto>> EmpresaById(int id)
        {
            var response = new BaseResponse<EmpresaResponseDto>();
            try
            {
                var Empresa = await _unitOfWork.Empresa.GetByIdAsync(id);

                if (Empresa is not null)
                {
                    response.IsSuccess = true;
                    response.Data = _mapper.Map<EmpresaResponseDto>(Empresa);
                    response.Message = ReplyMessage.MESSAGE_QUERY;
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
                }
            }
            catch (Exception)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_EXCEPTION;
            }

            return response;
        }

        public async Task<BaseResponse<bool>> RegisterEmpresa(EmpresaRequestDto requestDto)
        {
            var response = new BaseResponse<bool>();
            try
            {
                var Empresa = _mapper.Map<TbEmpresa>(requestDto);
                response.Data = await _unitOfWork.Empresa.RegisterAsync(Empresa);
                if (response.Data)
                {
                    var config = new ProducerConfig { BootstrapServers = "190.113.124.155:9092" };

                    using var producer = new ProducerBuilder<Null, string>(config).Build();

                    try
                    {
                        var response2 = await producer.ProduceAsync("empresaRegistrado", new Message<Null, string> { Value = JsonConvert.SerializeObject(Empresa) });
                        response.IsSuccess = true;
                        response.Message = ReplyMessage.MESSAGE_SAVE;
                    }
                    catch (ProduceException<Null, string> ex)
                    {
                        response.IsSuccess = false;
                        response.Message = ReplyMessage.MESSAGE_FAILED;
                        WatchLogger.Log(ex.Message);
                    }
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = ReplyMessage.MESSAGE_FAILED;
                }
            }
            catch (Exception)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_EXCEPTION;
            }

            return response;
        }

        public async Task<BaseResponse<bool>> EditEmpresa(int id, EmpresaRequestDto requestDto)
        {
            var response = new BaseResponse<bool>();
            try
            {
                var EmpresaEdit = await EmpresaById(id);

                if (EmpresaEdit.Data is null)
                {
                    response.IsSuccess = false;
                    response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
                    return response;
                }

                var Empresa = _mapper.Map<TbEmpresa>(requestDto);
                Empresa.Id = id;
                response.Data = await _unitOfWork.Empresa.EditAsync(Empresa);

                if (response.Data)
                {
                    var config = new ProducerConfig { BootstrapServers = "190.113.124.155:9092" };

                    using var producer = new ProducerBuilder<Null, string>(config).Build();

                    try
                    {
                        var response2 = await producer.ProduceAsync("empresaActualizado", new Message<Null, string> { Value = JsonConvert.SerializeObject(Empresa) });
                        response.IsSuccess = true;
                        response.Message = ReplyMessage.MESSAGE_UPDATE;
                    }
                    catch (ProduceException<Null, string> ex)
                    {
                        response.IsSuccess = false;
                        response.Message = ReplyMessage.MESSAGE_FAILED;
                        WatchLogger.Log(ex.Message);
                    }
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = ReplyMessage.MESSAGE_FAILED;
                }
            }
            catch (Exception)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_EXCEPTION;
            }

            return response;
        }

        public async Task<BaseResponse<bool>> RemoveEmpresa(int id)
        {
            var response = new BaseResponse<bool>();
            try
            {
                var Empresa = await EmpresaById(id);

                if (Empresa.Data is null)
                {
                    response.IsSuccess = false;
                    response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
                    return response;
                }

                response.Data = await _unitOfWork.Empresa.RemoveAsync(id);

                if (response.Data)
                {
                    var config = new ProducerConfig { BootstrapServers = "190.113.124.155:9092" };

                    using var producer = new ProducerBuilder<Null, string>(config).Build();

                    try
                    {
                        var response2 = await producer.ProduceAsync("empresaEliminado", new Message<Null, string> { Value = JsonConvert.SerializeObject(Empresa) });
                        response.IsSuccess = true;
                        response.Message = ReplyMessage.MESSAGE_DELETE;
                    }
                    catch (ProduceException<Null, string> ex)
                    {
                        response.IsSuccess = false;
                        response.Message = ReplyMessage.MESSAGE_FAILED;
                        WatchLogger.Log(ex.Message);
                    }
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = ReplyMessage.MESSAGE_FAILED;
                }
            }
            catch (Exception)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_EXCEPTION;
            }

            return response;
        }
    }
}