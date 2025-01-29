using AutoMapper;
using Aplication.Interfaces;
using Domain.Common;
using Domain.DTO;
using Domain.Entities;
using Domain.Interface;
using Aplication.TransformBase;
using Microsoft.Extensions.Logging;
using Aplication.Transform;


namespace Aplication.Services
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
            _logger.LogInformation("Executing owner create request{ Request}", Request);
            try
            {
                if (Request is not null)
                {

                    var producto = mapper.Map<Owner>(Request);
                    await repository.Insert(producto);
                    outPut.SetDataResponse(System.Net.HttpStatusCode.OK, "Buildings  created successs");                    
                    return outPut;
                }
                outPut.SetDataResponse(System.Net.HttpStatusCode.BadRequest, "Request error");

                _logger.LogInformation("Final execute owner create request{ Request}", Request);
            }
            catch (Exception ex)
            {
                outPut.SetDataResponse(System.Net.HttpStatusCode.InternalServerError, ex.Message);
                _logger.LogError(ex.Message);
            }

            return outPut;
        }


    }
}
