using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Net.Http.Headers;

namespace Core.HttpDynamo
{
    public class HttpDynamo
    {

        public async static Task<T?> GetRequestAsync<T>(IHttpClientFactory _httpClientFactory, string url, string bearerToken)
        {
            var result = await BuildRequest<T>(_httpClientFactory, url, bearerToken);

            return result;
        }

        public async static Task<T?> GetRequestAsync<T>(IHttpClientFactory _httpClientFactory, string url)
        {
            var result = await BuildRequest<T>(_httpClientFactory, url, null);

            return result;
        }

        public async static Task<Stream?> GetRequestAsync(IHttpClientFactory _httpClientFactory, string url, string bearerToken)
        {
            var result = await BuildRequest(_httpClientFactory, url, bearerToken);

            return result;
        }

        public async static Task<Stream?> GetRequestAsync(IHttpClientFactory _httpClientFactory, string url)
        {
            var result = await BuildRequest(_httpClientFactory, url, null);

            return result;
        }

        private static async Task<T?> BuildRequest<T>(IHttpClientFactory _httpClientFactory, string url, string? bearerToken)
        {
            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, url);

            var httpClient = _httpClientFactory.CreateClient();

            if (bearerToken != null)
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", bearerToken);

            var httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);

            var result = default(T);

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();
                result = await JsonSerializer.DeserializeAsync<T>(contentStream);
            }

            return result;
        }

        private static async Task<Stream?> BuildRequest(IHttpClientFactory _httpClientFactory, string url, string? bearerToken)
        {
            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, url);

            var httpClient = _httpClientFactory.CreateClient();

            if(bearerToken != null)
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", bearerToken);

            var httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                return await httpResponseMessage.Content.ReadAsStreamAsync();
            }

            return null;
        }
    }
}
