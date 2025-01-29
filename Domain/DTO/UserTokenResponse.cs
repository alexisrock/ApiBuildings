using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dto
{
    public class UserTokenResponse: BaseResponse
    {

        public string? Token { get; set; }
        public int Iduser { get; set; }
        public string? Username { get; set;}
        public int IdRol { get; set; }
        public int IdSesion { get; set; }
    }
}
