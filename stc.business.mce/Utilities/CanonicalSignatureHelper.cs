using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;

namespace stc.business.mce.Utilities
{
    public static class CanonicalSignatureHelper
    {
        public static string CalculateSignature(HttpRequestMessage request, object body, string accessKey, string secretKey)
        {
            var canonicalRequest = GetCanonicalRequest(request, accessKey, body != null ? JsonConvert.SerializeObject(body).ToString() : string.Empty);

            var result = $"{accessKey}:{canonicalRequest.HashHMAC(secretKey).ToLower()}";
            return result;
        }

        private static string GetCanonicalRequest(HttpRequestMessage request, string accessKey, string body)
        {
            var canonicalRequest = new StringBuilder();
            canonicalRequest.AppendFormat("{0}\n", request.Method.Method.ToUpper());
            canonicalRequest.AppendFormat("{0}\n", request.RequestUri.AbsolutePath);
            canonicalRequest.AppendFormat("{0}\n", GetCanonicalHeaders(request, null));

            if (body == null)
            {
                canonicalRequest.AppendFormat("{0}", accessKey);
            }
            else
            {
                canonicalRequest.AppendFormat("{0}\n", accessKey);
                canonicalRequest.AppendFormat("{0}", body);
            }

            return canonicalRequest.ToString();
        }

        private static string GetCanonicalHeaders(HttpRequestMessage request, IEnumerable<string> signedHeaders)
        {
            var headers = request.Headers.ToDictionary(x => x.Key.Trim().ToLowerInvariant(), x => string.Join(" ", x.Value).Trim());

            if (request.Content != null)
            {
                var contentHeaders = request.Content.Headers.ToDictionary(x => x.Key.Trim().ToLowerInvariant(), x => string.Join(" ", x.Value).Trim());
                foreach (var contentHeader in contentHeaders)
                {
                    if (contentHeader.Key.ToLower() != "content-type")
                    {
                        headers.Add(contentHeader.Key, contentHeader.Value);
                    }
                }
            }

            var sortedHeaders = new SortedDictionary<string, string>(headers);

            var canonicalHeaders = new StringBuilder();
            var validSignedHeaders = sortedHeaders.Where(header => signedHeaders == null || signedHeaders.Contains(header.Key));
            foreach (var header in validSignedHeaders)
            {
                if (header.Key != validSignedHeaders.Last().Key)
                {
                    canonicalHeaders.AppendFormat("{0}\n", header.Value);
                }
                else
                {
                    canonicalHeaders.AppendFormat("{0}", header.Value);
                }
            }
            return canonicalHeaders.ToString();
        }
    }
}
