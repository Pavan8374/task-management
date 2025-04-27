using System.Net;

namespace TaskManagement.Application.Common
{
    public class ResponseModel
    {
        public HttpStatusCode StatusCode { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
    }
}
