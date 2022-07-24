using Common.Enums;
using System.Text.Json.Serialization;

namespace Common.Model
{
    public sealed class LightingOutput : Output
    {
        public LightingOutput() : base()
        { }

        public LightingOutput(int trayNumber, DateTime startDate, LightIntensity lightIntensity) : base(trayNumber, startDate)
        {
            this.LightIntensity = lightIntensity;
        }

        [JsonPropertyName("lightIntensity")]
        public LightIntensity LightIntensity { get; set; }

        public override string ToString()
        {
            return $"{base.ToString()} {this.LightIntensity}";
        }
    }
}
