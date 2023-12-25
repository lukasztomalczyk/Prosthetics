namespace Prosthetics.Common
{
    public interface IHttpRequestExecutor
    {
        Task<TResult> GetAsync<TResult>(string url, string? clientName = null);
        Task<TResult> PostAsync<TResult, TBody>(string url, TBody body, string? clientName = null);
        Task DeleteAsync(string url, string? clientName = null);
        Task<TResult> PutAsync<TResult, TBody>(string url, TBody body, string? clientName = null);
    }
    public class HttpRequestExecutor : IHttpRequestExecutor
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IJsonConverter _jsonConverter;

        public HttpRequestExecutor(IHttpClientFactory httpClientFactory, IJsonConverter jsonConverter)
        {
            _httpClientFactory = httpClientFactory;
            _jsonConverter = jsonConverter;
        }

        public async Task DeleteAsync(string url, string? clientName = null)
        {
            await PerformHttpClientActionAsync(clientName, httpClient => httpClient.DeleteAsync(url));
        }

        public async Task<TResult> GetAsync<TResult>(string url, string? clientName = null)
        {
            var response = await PerformHttpClientActionAsync(clientName, httpClient => httpClient.GetAsync(url));

            return await DeserializeResponseAsync<TResult>(response);
        }

        public async Task<TResult> PostAsync<TResult, TBody>(string url, TBody body, string? clientName = null)
        {
            var jsonBody = ConvertToStringContent(body);

            var response = await PerformHttpClientActionAsync(clientName, httpClient => httpClient.PostAsync(url, jsonBody));

            return await DeserializeResponseAsync<TResult>(response);
        }

        public async Task<TResult> PutAsync<TResult, TBody>(string url, TBody body, string? clientName = null)
        {
            var jsonBody = ConvertToStringContent(body);

            var response = await PerformHttpClientActionAsync(clientName, httpClient => httpClient.PutAsync(url, jsonBody));

            return await DeserializeResponseAsync<TResult>(response);
        }

        private async Task HandleResponseAsync(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                throw new Exception($"Http request failed: {response.StatusCode}, with content {content}");
            }
        }

        private async Task<TResult> DeserializeResponseAsync<TResult>(HttpResponseMessage response)
        {
            var json = await response.Content.ReadAsStringAsync();

            return _jsonConverter.Deserialize<TResult>(json);
        }

        private StringContent ConvertToStringContent<TBody>(TBody? body)
        {
            return new StringContent(_jsonConverter.Serialize(body));
        }

        private async Task<HttpResponseMessage> PerformHttpClientActionAsync(string? clientName, Func<HttpClient, Task<HttpResponseMessage>> func)
        {
            var client = ConfigureClientByName(clientName);

            var response = await func.Invoke(client);

            await HandleResponseAsync(response);

            return response;
        }

        private HttpClient ConfigureClientByName(string? clientName)
        {
            if (clientName == null)
                return _httpClientFactory.CreateClient();

            return _httpClientFactory.CreateClient(clientName);
        }
    }
}
