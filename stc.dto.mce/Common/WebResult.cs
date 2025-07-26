using Core.DTO.Response;

namespace stc.dto.mce.Common
{
    public class WebResult<T>
    {
        //[JsonIgnore]
        //public CRUDStatusCodeRes StatusCode { get; set; }
        //[JsonIgnore]
        //public string ErrorMessage { get; set; }
        public T Data { get; set; }
    }
}
