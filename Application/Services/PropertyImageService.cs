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
using Microsoft.Extensions.Logging;

namespace Application.Services
{
    /// <summary>
    /// This class represent all service of Property Image
    /// 
    /// </summary>
    public class PropertyImageService : IPropertyImageService
    {

        BaseResponse outPut;
        private readonly IPropertyImagesRepository propertyImagesRepository;
        private readonly ILogger<PropertyImageService> _logger;


        public PropertyImageService(IPropertyImagesRepository _propertyImagesRepository, ILogger<PropertyImageService> logger)
        {
            outPut = new BaseResponse();
            propertyImagesRepository = _propertyImagesRepository;
            _logger = logger;
        }
        public async Task<BaseResponse> Update(PropertyImagesUpdateRequest request)
        {
            try
            {
                _logger.LogInformation("Executing PropertyImage Update request{ request}", request);
                var result = await propertyImagesRepository.GetPropertyImageById(request.IdPropertyImage);
                if (result is not null)
                {

                    await this.propertyImagesRepository.UpdatePropertyImage(result.GetPropertyImages(request));
                    outPut.SetDataResponse(System.Net.HttpStatusCode.OK, "The property Images  was update with success");
                    _logger.LogInformation("Final execute PropertyImage Update request{ Request}", request);
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
