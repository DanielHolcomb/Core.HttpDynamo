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


        public async static Task<T?> GetRequestAsync<T>(IHttpClientFactory _httpClientFactory, string url)
        {
            var result = await GetRequestAsync<T>(_httpClientFactory, url, null, null);

            return result;
        }

        public async static Task<T?> GetRequestAsync<T>(IHttpClientFactory _httpClientFactory, string url, Dictionary<string, string>? headers)
        {
            var result = await GetRequestAsync<T>(_httpClientFactory, url, null, headers);

            return result;
        }

        public async static Task<T?> GetRequestAsync<T>(IHttpClientFactory _httpClientFactory, string url, string? bearerToken, Dictionary<string, string>? headers)
        {
            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, url);

            var httpClient = _httpClientFactory.CreateClient();

            if (bearerToken != null)
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", bearerToken);

            if(headers != null)
                foreach(var header in headers)
                    httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);

            var httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);

            var result = default(T);

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();
                result = await JsonSerializer.DeserializeAsync<T>(contentStream);
            }

            return result;
        }

        public async static Task<Stream?> GetRequestAsync(IHttpClientFactory _httpClientFactory, string url)
        {
            var result = await GetRequestAsync(_httpClientFactory, url, null, null);

            return result;
        }

        public async static Task<Stream?> GetRequestAsync(IHttpClientFactory _httpClientFactory, string url, Dictionary<string, string>? headers)
        {
            var result = await GetRequestAsync(_httpClientFactory, url, null, headers);

            return result;
        }

        public async static Task<Stream?> GetRequestAsync(IHttpClientFactory _httpClientFactory, string url, string? bearerToken, Dictionary<string, string>? headers)
        {
            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, url);

            var httpClient = _httpClientFactory.CreateClient();

            if (bearerToken != null)
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", bearerToken);

            if (headers != null)
                foreach (var header in headers)
                    httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);

            var httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                return await httpResponseMessage.Content.ReadAsStreamAsync();
            }

            return null;
        }

        public async static Task<T?> PostRequestAsync<T>(IHttpClientFactory _httpClientFactory, string url, object payload)
        {
            var result = await PostRequestAsync<T>(_httpClientFactory, url, null, payload);

            return result;
        }

        public async static Task<T?> PostRequestAsync<T>(IHttpClientFactory _httpClientFactory, string url, string? bearerToken, object payload)
        {
            var json = JsonSerializer.Serialize(payload);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var httpClient = _httpClientFactory.CreateClient();

            if (bearerToken != null)
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", bearerToken);

            var httpResponseMessage = await httpClient.PostAsync(url, content);

            var result = default(T);

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();

                result = await JsonSerializer.DeserializeAsync<T>(contentStream);
            }

            return result;
        }

        public async static Task<Stream?> PostRequestAsync(IHttpClientFactory _httpClientFactory, string url, object payload)
        {
            var result = await PostRequestAsync(_httpClientFactory, url, null, payload);

            return result;
        }

        public async static Task<Stream?> PostRequestAsync(IHttpClientFactory _httpClientFactory, string url, string? bearerToken, object payload)
        {
            var json = JsonSerializer.Serialize(payload);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var httpClient = _httpClientFactory.CreateClient();

            if (bearerToken != null)
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", bearerToken);

            var httpResponseMessage = await httpClient.PostAsync(url, content);

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                return await httpResponseMessage.Content.ReadAsStreamAsync();
            }

            return null;
        }
    }
}
