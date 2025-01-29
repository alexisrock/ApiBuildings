using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Common
{
    public class BaseResponse
    {

        public HttpStatusCode StatusCode { get; set; }
        public string? Message { get; set; }
    }
}
