using System.Net;

namespace stc.dto.mce.Response
{
    public class ApiHelperRes<T>
    {
        public HttpStatusCode StatusCode { get; set; }

        public string Message { get; set; }

        public T Data { get; set; }
    }
}
