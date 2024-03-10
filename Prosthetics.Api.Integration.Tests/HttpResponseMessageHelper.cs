using FluentAssertions;
using Newtonsoft.Json;

namespace Prosthetics.Api.Integration.Tests
{
    public static class HttpResponseMessageHelper
    {
        public static void CheckResponse(this HttpResponseMessage response)
        {
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            response.Content.Headers.ContentType.ToString().Should().Be("application/json; charset=utf-8");
        }

        public static async Task<TResult> ConvertResponseAsync<TResult>(this HttpResponseMessage response)
        {
            var json = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<TResult>(json);
        }
    }
}
