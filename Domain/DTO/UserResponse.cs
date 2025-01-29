using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dto
{
    public class UserResponse:BaseResponse
    {
        public string UserName { get; set; } = string.Empty;    
    }
    
}
