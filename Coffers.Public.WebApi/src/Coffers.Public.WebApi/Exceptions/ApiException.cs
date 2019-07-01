using System;
using System.Collections.Generic;
using System.Net;

namespace Coffers.Public.WebApi.Exceptions
{
    public class ApiException : Exception
    {
        private readonly HttpStatusCode _httpCode;

        public ApiException(HttpStatusCode httpCode, string code, string message)
            : this(httpCode, code, message, null)
        {
            _httpCode = httpCode;
        }

        public ApiException(HttpStatusCode httpCode, string code, string message, Exception innerException = null)
            : base(message, innerException)
        {
            Code = code;
            _httpCode = httpCode;
        }

        public ApiException(HttpStatusCode httpCode, string code, string message, IDictionary<string, object> fields, Exception innerException = null)
            : base(message, innerException)
        {
            _httpCode = httpCode;
            Code = code;
            Fields = fields;
        }

        public int GetHttpStatusCode() => (int)_httpCode;

        public string Code { get; }

        public IDictionary<string, object> Fields { get; set; }
    }
}