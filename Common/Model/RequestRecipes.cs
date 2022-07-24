using System.Text.Json.Serialization;

namespace Common.Model
{
    public sealed class RequestRecipes
    {
        [JsonPropertyName("recipes")]
        public ICollection<Recipe> Recipes { get; set; }
    }
}
