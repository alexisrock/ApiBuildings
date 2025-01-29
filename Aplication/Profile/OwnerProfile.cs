using Domain.DTO;
using Domain.Entities;

namespace Aplication.Profile
{
    public class OwnerProfile : AutoMapper.Profile
    {

        public OwnerProfile() 
        {

            CreateMap<Owner, OwnerResponse>()
             .ReverseMap();

            CreateMap<Owner, OwnerRequest>()
            .ReverseMap();


        }
    }
}
