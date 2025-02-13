using AutoMapper;
using Domain.Dto;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Profile
{
    internal class UsuarioProfile: AutoMapper.Profile
    {

        public UsuarioProfile()
        {
            CreateMap<UserRequest, Users>()
                .ReverseMap();

            CreateMap<UserTokenResponse, Users>()
               .ReverseMap();

            CreateMap<UserResponse, Users>()
               .ReverseMap();
        }
    }
}
