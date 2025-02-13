using Domain.Common;
using Domain.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IUserService
    {
        Task<UserTokenResponse> GetAuthentication(UserTokenRequest userTokenRequest);
        Task<UserResponse> CreateUser(UserRequest userRequest);
      


    }
}
