using Newtonsoft.Json;

namespace Prosthetics.Common
{
    public interface IJsonConverter
    {
        TResult Deserialize<TResult>(string json);
        string Serialize<TInput>(TInput value);
    }

    public class JsonConverter : IJsonConverter
    {
        private readonly JsonSerializerSettings _settings;

        public JsonConverter(JsonSerializerSettings settings)
        {
            _settings = settings;
        }

        public TResult Deserialize<TResult>(string json)
            => JsonConvert.DeserializeObject<TResult>(json, _settings);

        public string Serialize<TInput>(TInput value)
            => JsonConvert.SerializeObject(value, _settings);
    }
}
