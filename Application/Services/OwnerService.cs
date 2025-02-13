using AutoMapper;
using Application.Interfaces;
using Domain.Common;
using Domain.DTO;
using Domain.Entities;
using Domain.Interface;
using Application.TransformBase;
using Microsoft.Extensions.Logging;
using Application.Transform;
using Domain.Exceptions;


namespace Application.Services
{
    /// <summary>
    /// This class represent all service of owner
    /// 
    /// </summary>
    public class OwnerService : IOwnerService
    {

        private readonly IRepository<Owner> repository;
        private readonly ILogger<OwnerService> _logger;
        private readonly IMapper mapper;

        public OwnerService(IRepository<Owner> repository, IMapper mapper, ILogger<OwnerService> logger)
        {
            this.repository = repository;
            this.mapper = mapper;
            _logger = logger;
        }

        public async Task<List<OwnerResponse>> GetAll()
        {
            _logger.LogInformation("Executing owner GetAll");
            var listPruebaSeleccion = await repository.GetAll();
            var list = listPruebaSeleccion.GetListOwnerResponse();
            return list;
        }
  
        public async Task<BaseResponse> Create(OwnerRequest Request)
        {
            var outPut = new BaseResponse();
            _logger.LogInformation("Executing owner create request {Request}", Request);
            try
            {
                if (Request is not null)
                {

                    var producto = mapper.Map<Owner>(Request);
                    await repository.Insert(producto);
                    outPut.SetDataResponse(System.Net.HttpStatusCode.OK, "Buildings  created successs");                    
                    return outPut;
                }
                _logger.LogInformation("Final execute owner create request {Request}", Request);
                throw new NotFoundException("Request error");


            }
            catch (Exception ex) when (ex is ApiException)
            {
                throw;
            }
            catch (Exception ex)
            {
                 
                var message = ex.Message ?? ex.InnerException.Message;
                _logger.LogError(message);
                throw new ApiException("Ocurrió un error inesperado", (int)System.Net.HttpStatusCode.InternalServerError);
               
            }
           
        }


    }
}
