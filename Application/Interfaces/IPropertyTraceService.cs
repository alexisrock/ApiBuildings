using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Common;
using Domain.DTO;

namespace Application.Interfaces
{
    public interface IPropertyTraceService
    {

        Task<BaseResponse> Update(PropertyTraceUpdateRequest request);
    }
}
