using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using Application.Transform;
using Application.TransformBase;
using AutoMapper;
using Domain.Common;
using Domain.DTO;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interface;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.Services
{
    /// <summary>
    /// This class represent all service of Property
    /// 
    /// </summary>
    public class PropertyService: IPropertyService
    {

        private readonly IPropertyRepository  repositoryProperty;
        private readonly ILogger<PropertyService> _logger;
        private readonly IMapper mapper;
        BaseResponse outPut;

        public PropertyService(IPropertyRepository repositoryProperty, IMapper mapper, ILogger<PropertyService> logger)
        {
            this.repositoryProperty = repositoryProperty;
            this._logger = logger;
            this.mapper = mapper;
            outPut = new BaseResponse();
        }


        public async Task<List<PropertyResponse>> GetPropertiesAll(FiltroProperty filtroObj)
        {
             
            Expression<Func<Property, bool>> filtro = p => true;

            if (!string.IsNullOrEmpty(filtroObj.CodeInternal))
            {
                filtro = p => p.CodeInternal.Contains(filtroObj.CodeInternal);
            }

            if (filtroObj.Year > 0)
            {
                filtro = p => p.Year.Equals(filtroObj.Year);
            }

            if (!string.IsNullOrEmpty(filtroObj.Name))
            {
                filtro = p => p.Name.Contains(filtroObj.Name);
            }

            if (filtroObj.IdOwner > 0)
            {
                filtro = p => p.IdOwner.Equals(filtroObj.IdOwner);
            }


            var listProperty = await this.repositoryProperty.GetPropertyAll(filtro, filtroObj.Pagina, filtroObj.TamanioPagina);

            var list = listProperty.GetPropertyAll();
            return list;
        }     

        public async Task<BaseResponse> Create(PropertyRequest request)
        {
            try
            {
                _logger.LogInformation("Executing property create request {Request}", request);
                var property = mapper.Map<Property>(request);
                var propertyImages = mapper.Map<PropertyImage>(request);
                var propertyTrace = mapper.Map<PropertyTrace>(request);
                var result = await this.repositoryProperty.GetTransaction(property, propertyImages, propertyTrace);
                if (result == true)
                {
                    outPut.SetDataResponse(System.Net.HttpStatusCode.OK, "The property  was create with success");
                    _logger.LogInformation("Final execute Property Create request {Request}", request);
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

        public async Task<BaseResponse> Update(PropertyUpdateRequest request)
        {
            try
            {
                _logger.LogInformation("Executing Property Update request {Request}", request);

                var result = await this.repositoryProperty.GetPropertyById(request.IdProperty);
                if (result is not null )
                {

                    await this.repositoryProperty.updateProperty(result.GetProperty(request));
                    outPut.SetDataResponse(System.Net.HttpStatusCode.OK, "The property  was update with success");
                    _logger.LogInformation("Final execute Property Update request {Request}", request);

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
