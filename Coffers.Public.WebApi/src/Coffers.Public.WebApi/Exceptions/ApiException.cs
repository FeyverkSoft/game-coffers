using System;
using System.Collections.Generic;
using System.Net;

namespace Coffers.Public.WebApi.Exceptions
{
    public class ApiException : Exception
    {
        private readonly HttpStatusCode _httpCode;

        public ApiException(HttpStatusCode httpCode, String code, String message)
            : this(httpCode, code, message, null)
        {
            _httpCode = httpCode;
        }

        public ApiException(HttpStatusCode httpCode, String code, String message, Exception innerException = null)
            : base(message, innerException)
        {
            Code = code;
            _httpCode = httpCode;
        }

        public ApiException(HttpStatusCode httpCode, String code, String message, IDictionary<String, Object> fields, Exception innerException = null)
            : base(message, innerException)
        {
            _httpCode = httpCode;
            Code = code;
            Fields = fields;
        }

        public Int32 GetHttpStatusCode() => (Int32)_httpCode;

        public String Code { get; }

        public IDictionary<String, Object> Fields { get; set; }
    }
}