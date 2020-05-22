using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Musicio.Client
{
    public static class HttpClientExtensions
    {
        private const string ConnectionString = "https://localhost:5001/";
        
        public static string WebToken { get; set; }
        public static async Task<T> PostJsonAsync<T>(this HttpClient httpClient, string url, object data) => await httpClient.SendJsonAsync<T>(HttpMethod.Post, url, data);
        public static async Task<T> PutJsonAsync<T>(this HttpClient httpClient, string url, object data) => await httpClient.SendJsonAsync<T>(HttpMethod.Put, url, data);
        public static async Task<T> GetJsonAsync<T>(this HttpClient httpClient, string url)
        {
            Console.WriteLine(WebToken);
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", WebToken);
            var response = await httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Get, ConnectionString + url));

            var stringContent = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(stringContent);
        }

        public static async Task<T> SendJsonAsync<T>(this HttpClient httpClient, HttpMethod method, string url, object data)
        {
            Console.WriteLine(WebToken);
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", WebToken);
            var response = await httpClient.SendAsync(new HttpRequestMessage(method, ConnectionString + url)
            {
                Content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json")
            });

            var stringContent = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(stringContent);
        }
    }
}
