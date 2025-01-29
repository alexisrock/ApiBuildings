using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Common;
using Domain.DTO;

namespace Aplication.Interfaces
{
    public  interface IPropertyImageService
    {
        Task<BaseResponse> Update(PropertyImagesUpdateRequest request);
    }
}
