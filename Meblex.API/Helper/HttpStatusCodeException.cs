using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Meblex.API.Helper
{
    public class HttpStatusCodeException :Exception
    {
        public HttpStatusCode StatusCode { get; set; }
        public string Message { get; set; }
        public HttpStatusCodeException(HttpStatusCode statusCode)
        {
            StatusCode = statusCode;
            Message = "";
        }

        public HttpStatusCodeException(HttpStatusCode statusCode, string message) :base(message)
        {
            StatusCode = statusCode;
            Message = message;
        }
    }
}
