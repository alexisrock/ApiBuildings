using AutoMapper;
using Domain.Common;
using Domain.DTO;
using Domain.Entities;
using Domain.Interface;

namespace Application.Interfaces
{
    public interface IPropertyService
    {


        Task<List<PropertyResponse>> GetPropertiesAll(FiltroProperty filtro);
        Task<BaseResponse> Create(PropertyRequest request);
        Task<BaseResponse> Update(PropertyUpdateRequest request);
    }
}
