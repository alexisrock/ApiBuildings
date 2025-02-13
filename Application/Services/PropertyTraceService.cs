using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using Application.Transform;
using Application.TransformBase;
using Domain.Common;
using Domain.DTO;
using Domain.Exceptions;
using Domain.Interface;
using Infrastructure.Repository;
using Microsoft.Extensions.Logging;

namespace Application.Services
{

    /// <summary>
    /// This class represent all service of Property trace
    /// 
    /// </summary>
    public class PropertyTraceService : IPropertyTraceService
    {
        BaseResponse outPut;
        private readonly IPropertyTraceRepository _propertyTraceRepository;
        private readonly ILogger<PropertyTraceService> _logger;

        public PropertyTraceService(IPropertyTraceRepository propertyTraceRepository, ILogger<PropertyTraceService> logger)
        {
            outPut = new BaseResponse();
            _propertyTraceRepository = propertyTraceRepository;
            _logger = logger;
        }

        public async Task<BaseResponse> Update(PropertyTraceUpdateRequest request)
        {
            try
			{
                _logger.LogInformation("Executing Property trace Update request {Request}", request);

                var result = await _propertyTraceRepository.GetPropertyTraceById(request.IdPropertyTrace);
                if (result is not null)
                {

                    await this._propertyTraceRepository.UpdatePropertyTrace(result.GetPropertyTrace(request));
                    outPut.SetDataResponse(System.Net.HttpStatusCode.OK, "The property Trace  was update with success");
                    _logger.LogInformation("Final execute Property trace Update request {Request}", request);

                    return outPut;
                }


                throw new NotFoundException("Request error");
            }
            catch (Exception ex) when (ex is ApiException)
            {
                throw;
            }
            catch (Exception ex)
            {
                 
                _logger.LogError(ex.Message);
                throw new ApiException("Ocurrió un error inesperado", (int)System.Net.HttpStatusCode.InternalServerError);
            }
           
        }
    }
}
