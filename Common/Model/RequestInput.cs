using System.Text.Json.Serialization;

namespace Common.Model
{
    public sealed class RequestInput
    {
        [JsonPropertyName("input")]
        public ICollection<Input> Input { get; set; }
    }
}
