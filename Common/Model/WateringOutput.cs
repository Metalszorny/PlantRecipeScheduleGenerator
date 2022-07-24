using System.Text.Json.Serialization;

namespace Common.Model
{
    public sealed class WateringOutput : Output
    {
        public WateringOutput() : base()
        { }

        public WateringOutput(int trayNumber, DateTime startDate, short waterAmount) : base(trayNumber, startDate)
        {
            this.WaterAmount = waterAmount;
        }

        [JsonPropertyName("waterAmount")]
        public short WaterAmount { get; set; }

        public override string ToString()
        {
            return $"{base.ToString()} {this.WaterAmount}";
        }
    }
}
