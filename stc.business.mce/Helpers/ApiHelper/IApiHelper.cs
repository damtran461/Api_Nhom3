using stc.dto.mce.Response;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace stc.business.mce.Helpers
{
    public interface IApiHelper : IDisposable
    {
        Task<ApiHelperRes<T>> GetAsync<T>(string url, object parameters = null);
        Task<ApiHelperRes<T>> PostAsync<T>(string url, object dataToPost = null);
        Task<ApiHelperRes<T>> PostFileAsync<T>(string url, MultipartFormDataContent formDataContent);
        Task<ApiHelperRes<T>> DeleteAsync<T>(string url);
        Task<ApiHelperRes<T>> PutAsync<T>(string url, object dataToPut = null);
        Task<byte[]> GetFileAsync(string url);
        bool IsERPToken { get; set; }
    }
}
