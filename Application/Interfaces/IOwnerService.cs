using Domain.Common;
using Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IOwnerService
    {

        Task<List<OwnerResponse>> GetAll();              
        Task<BaseResponse> Create(OwnerRequest pruebaSeleccionRequest);
    
    }
}
