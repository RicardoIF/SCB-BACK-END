using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Shared.RequestFeatures
{
    public class ApiResponse<T> : OperationResult
    {
        public ApiResponse(T data)
        {
            Data = data;
        }
        public T Data { get; set; }
        public MetaData Meta { get; set; }

        public ApiResponse()
        {

        }
        public ApiResponse(HttpStatusCode statusCode)
        : base(statusCode)
        {
        }

        public ApiResponse(bool success, HttpStatusCode statusCode)
            : base(success, statusCode)
        {
        }
    }

    public class OperationResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public OperationResult()
        {
            Success = false;
            Message = String.Empty;
            StatusCode = 0;
        }

        public OperationResult(HttpStatusCode statusCode)
        {
            Success = false;
            StatusCode = statusCode;
            Message = String.Empty;
        }

        public OperationResult(bool success, HttpStatusCode statusCode)
        {
            Success = success;
            StatusCode = statusCode;
            Message = String.Empty;
        }
    }
}

