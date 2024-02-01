using System.Text.Json;

namespace Prosthetics.Common
{
    public interface IJsonConverter
    {
        TResult Deserialize<TResult>(string json);
        string Serialize<TInput>(TInput value);
    }

    public class JsonConverter : IJsonConverter
    {
        private readonly JsonSerializerOptions _settings;

        public JsonConverter(JsonSerializerOptions settings)
        {
            _settings = settings;
        }

        public TResult Deserialize<TResult>(string json)
            => JsonSerializer.Deserialize<TResult>(json, _settings);

        public string Serialize<TInput>(TInput value)
            => JsonSerializer.Serialize(value, _settings);
    }
}
