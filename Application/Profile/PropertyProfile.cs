using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.DTO;
using Domain.Entities;

namespace Application.Profile
{
    public class PropertyProfile : AutoMapper.Profile
    {


        public PropertyProfile() {

            CreateMap<PropertyRequest, Property>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
                .ForMember(dest => dest.CodeInternal, opt => opt.MapFrom(src => src.CodeInternal))
                .ForMember(dest => dest.Year, opt => opt.MapFrom(src => src.Year))
                .ForMember(dest => dest.IdOwner, opt => opt.MapFrom(src => src.IdOwner))
           .ReverseMap();


            CreateMap<PropertyRequest, PropertyImage>()
                .ForMember(dest => dest.FileImage, opt => opt.MapFrom(src => src.FileImage))
                .ForMember(dest => dest.Enabled, opt => opt.MapFrom(src => src.Enabled))
            .ReverseMap();


            CreateMap<PropertyRequest, PropertyTrace>()
                .ForMember(dest => dest.DateSale, opt => opt.MapFrom(src => src.DateSale))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.NameTrace))
                .ForMember(dest => dest.Tax, opt => opt.MapFrom(src => src.Tax))
                .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Value))
            .ReverseMap();

        }
    }
}
