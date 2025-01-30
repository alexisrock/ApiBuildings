using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aplication.Interfaces;
using Aplication.Transform;
using Aplication.TransformBase;
using Domain.Common;
using Domain.DTO;
using Domain.Interface;
using Infraestructure.Repository;
using Microsoft.Extensions.Logging;

namespace Aplication.Services
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



                outPut.SetDataResponse(System.Net.HttpStatusCode.BadRequest, "Bad Request");
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
