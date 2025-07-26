using stc.business.mce.Utilities;
using stc.dto.mce.Response;
using Core.Common;
using Core.Common.Extensions;
using Core.DataAccess.Extentions;
using Core.Log;
using Core.Log.Interface;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace stc.business.mce.Helpers
{
    public class ApiHelper : IApiHelper
    {
        private readonly HttpClient _client;
        private static string _token { get; set; }
        private static DateTime _expiryToken = DateTime.Now;
        public bool IsERPToken { get; set; } = false;

        public ApiHelper(HttpClient client)
        {
            _client = client;
        }

        private async Task Init()
        {          
            if (IsERPToken)
            {
                _token = await AuthenticateHelper.GetERPTokenByAccount();
            }
            else
            {
                _token = await ClientExtensions.GetTokenIDS();
            }

            _expiryToken = _token.GetExpiryToken();
        }

        public async Task<ApiHelperRes<T>> GetAsync<T>(string url, object parameters = null)
        {
            var query = parameters != null ? parameters.ToQueryString() : string.Empty;
            var requestMessage = await GetHttpRequestMessage(HttpMethod.Get, url + query);

            return await GetResources<T>(requestMessage, query);
        }

        public async Task<ApiHelperRes<T>> PostAsync<T>(string url, object dataToPost = null)
        {
            var requestMessage = await GetHttpRequestMessage(HttpMethod.Post, url);

            var objRequest = JsonConvert.SerializeObject(dataToPost, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            requestMessage.Content = new StringContent(objRequest, Encoding.UTF8, "application/json");

            return await GetResources<T>(requestMessage, objRequest);
        }

        public async Task<ApiHelperRes<T>> PostFileAsync<T>(string url, MultipartFormDataContent formDataContent)
        {
            var requestMessage = await GetHttpRequestMessage(HttpMethod.Post, url);
            requestMessage.Content = formDataContent;

            return await GetResources<T>(requestMessage);
        }

        public async Task<ApiHelperRes<T>> DeleteAsync<T>(string url)
        {
            var requestMessage = await GetHttpRequestMessage(HttpMethod.Delete, url);

            return await GetResources<T>(requestMessage, string.Empty);
        }

        public async Task<ApiHelperRes<T>> PutAsync<T>(string url, object dataToPut = null)
        {
            var requestMessage = await GetHttpRequestMessage(HttpMethod.Put, url);

            var objRequest = JsonConvert.SerializeObject(dataToPut, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            requestMessage.Content = new StringContent(objRequest, Encoding.UTF8, "application/json");

            return await GetResources<T>(requestMessage, objRequest);
        }

        private async Task<HttpRequestMessage> GetHttpRequestMessage(HttpMethod method, string url)
        {
            if (_expiryToken <= DateTime.Now)
            {
                await Init();
            }

            _client.DefaultRequestHeaders.Clear();
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var request = new HttpRequestMessage(method, url);

            return request;
        }

        private async Task<ApiHelperRes<T>> GetResources<T>(HttpRequestMessage requestMessage, string jsonBody = null)
        {
            var result = new ApiHelperRes<T>();
            var logger = Engine.ContainerManager.Resolve<ILogger>();
            var logIdentify = new LogIdentify()
            {
                ProcessID = Guid.NewGuid().ToString()
            };

            using (var response = await _client.SendAsync(requestMessage))
            {
                try
                {
                    result.StatusCode = response.StatusCode;
                    result.Message = await GetErrorAPI(response.Content, response.StatusCode);

                    if (string.IsNullOrEmpty(result.Message))
                    {
                        result.Data = await GetResponse<T>(response);
                    }

                    logger.Info(logIdentify, $"API: {requestMessage.RequestUri} " +
                             $"\r\nMethod: {requestMessage.Method}" +
                             $"\r\nHttpstatus: {response.StatusCode} " +
                             $"\r\nDateTime: {DateTime.Now} " +
                             $"\r\n\tReq: {jsonBody}");
                }
                catch (TaskCanceledException ex) when (ex.InnerException is TimeoutException)
                {
                    var logMessage = $"Lỗi gọi API: {requestMessage.RequestUri} " +
                        $"\r\nMethod: {requestMessage.Method}" +
                        $"\r\nHttpstatus: {response.StatusCode} " +
                        $"\r\nDateTime: {DateTime.Now} " +
                        $"\r\nException message: {ex.Message} " +
                        $"\r\n\tReq: {jsonBody}";

                    logger.Error(logIdentify, logMessage);

                    result.Message = "Lỗi hết thời gian xử lý yêu cầu";
                }
                catch (Exception ex)
                {
                    var logMessage = $"Lỗi gọi API: {requestMessage.RequestUri} " +
                        $"\r\nMethod: {requestMessage.Method}" +
                        $"\r\nHttpstatus: {response.StatusCode} " +
                        $"\r\nDateTime: {DateTime.Now} " +
                        $"\r\nException message: {ex.Message} " +
                        $"\r\n\tReq: {jsonBody}";

                    logger.Error(logIdentify, logMessage);

                    result.Message = ex.Message;
                }
            }

            return result;
        }

        private async Task<T> GetResponse<T>(HttpResponseMessage response)
        {
            using (var content = response.Content)
            {
                var responseData = await content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<T>(responseData);
            }
        }

        public async Task<byte[]> GetFileAsync(string url)
        {
            var requestMessage = await GetHttpRequestMessage(HttpMethod.Get, url);

            using (var result = await _client.SendAsync(requestMessage))
            {
                if (result.IsSuccessStatusCode)
                {
                    return await result.Content.ReadAsByteArrayAsync();
                }
            }

            return null;
        }

        private async Task<string> GetErrorAPI(HttpContent content, HttpStatusCode statusCode)
        {
            if (statusCode == HttpStatusCode.OK || statusCode == HttpStatusCode.Created)
            {
                return string.Empty;
            }

            var responseData = await content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<string>(responseData);
        }

        #region Dispose
        private bool _disposedValue;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    _client.Dispose();
                }
                _disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~ApiHelper()
        {
            Dispose(false);
        }
        #endregion
    }
}
