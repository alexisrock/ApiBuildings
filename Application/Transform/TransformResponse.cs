using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Domain.Common;

namespace Application.TransformBase
{
    internal static class TransformResponse
    {


        internal static BaseResponse SetDataResponse(this  BaseResponse objeto, HttpStatusCode StatusCode, string? Message)
        {
            objeto.StatusCode = StatusCode;
            objeto.Message = Message;
            return objeto;
        }



    }
}
