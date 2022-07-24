using System.Text.Json.Serialization;

namespace Common.Model
{
    public sealed class RequestParameters
    {
        [JsonPropertyName("fileName")]
        public string FileName { get; set; }
        [JsonPropertyName("folderPath")]
        public string FolderPath { get; set; }
        [JsonPropertyName("input")]
        public RequestInput Input { get; set; }
        [JsonPropertyName("recipes")]
        public RequestRecipes Recipes { get; set; }
    }
}
