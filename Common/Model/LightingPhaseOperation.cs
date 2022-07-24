using Common.Enums;
using System.Text.Json.Serialization;

namespace Common.Model
{
    public sealed class LightingPhaseOperation
    {
        public LightingPhaseOperation(short offsetHours, short offsetMinutes, LightIntensity lightIntensity)
        {
            OffsetHours = offsetHours;
            OffsetMinutes = offsetMinutes;
            LightIntensity = lightIntensity;
        }

        [JsonPropertyName("offsetHours")]
        public short OffsetHours { get; set; }
        [JsonPropertyName("offsetMinutes")]
        public short OffsetMinutes { get; set; }
        [JsonPropertyName("LightIntensity")]
        public LightIntensity LightIntensity { get; set; }
    }
}
