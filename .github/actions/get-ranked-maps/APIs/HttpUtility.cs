using System.Net;
using Newtonsoft.Json;

namespace get_ranked_maps.APIs
{
    internal static class HttpUtility
    {
        private static HttpClient? _httpClient;

        public static HttpClient HttpClient
        {
            get
            {
                if (_httpClient == null)
                {
                    var handler = new HttpClientHandler()
                    {
                        AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate,
                    };
                    _httpClient = new HttpClient(handler);
                }
                return _httpClient;
            }
        }

        public static async Task<T?> GetAndDeserialize<T>(string url)
        {
            var httpsResponse = await HttpClient.GetAsync(url);
            if (httpsResponse.IsSuccessStatusCode)
            {
                var responseContent = await httpsResponse.Content.ReadAsStringAsync();
                var detail = JsonConvert.DeserializeObject<T>(responseContent);
                return detail;
            }
            else
            {
                return default(T);
            }
        }
    }
}
