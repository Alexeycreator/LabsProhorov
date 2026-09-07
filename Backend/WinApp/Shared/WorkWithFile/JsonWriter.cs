using System.IO;
using Newtonsoft.Json;
using Shared.Models;

namespace Shared
{
    public sealed class JsonWriter
    {
        private static readonly JsonSerializerSettings DefaultSettings = new JsonSerializerSettings
        {
            Formatting = Formatting.Indented,
            NullValueHandling = NullValueHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Ignore
        };

        public void Write(string filePath, Root data, JsonSerializerSettings settings = null)
        {
            var jsonSettings = settings ?? DefaultSettings;
            var json = JsonConvert.SerializeObject(data, jsonSettings);
            File.WriteAllText(filePath, json);
        }
    }
}