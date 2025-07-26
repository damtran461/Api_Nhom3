using Core.DTO.Response;

namespace stc.dto.mce.Common
{
    public class ApiResult<T>
    {
        public CRUDStatusCodeRes code { get; set; }
        public string message { get; set; }
        public T data { get; set; }
    }
}
