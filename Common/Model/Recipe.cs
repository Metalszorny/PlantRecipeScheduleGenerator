using System.Text.Json.Serialization;

namespace Common.Model
{
    public sealed class Recipe
    {
        public Recipe(string Name)
        {
            this.Name = Name;
            this.LightingPhases = new List<LightingPhase>();
            this.WateringPhases = new List<WateringPhase>();
        }

        ~Recipe()
        {
            this.WateringPhases.Clear();
            this.LightingPhases.Clear();
        }

        [JsonPropertyName("name")]
        public string Name { get; }
        [JsonPropertyName("lightingPhases")]
        public ICollection<LightingPhase> LightingPhases { get; set; }
        [JsonPropertyName("wateringPhases")]
        public ICollection<WateringPhase> WateringPhases { get; set; }
    }
}
