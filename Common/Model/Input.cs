using System.Text.Json.Serialization;

namespace Common.Model
{
    public sealed class Input
    {
        [JsonPropertyName("trayNumber")]
        public int TrayNumber { get; set; }
        [JsonPropertyName("recipeName")]
        public string RecipeName { get; set; }
        [JsonPropertyName("startDate")]
        [JsonConverter(typeof(CustomDateTimeConverter))]
        public DateTime StartDate { get; set; }
    }
}
