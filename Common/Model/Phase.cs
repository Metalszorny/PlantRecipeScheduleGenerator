using System.Text.Json.Serialization;

namespace Common.Model
{
    public abstract class Phase
    {
        public Phase(string Name, short Order, short Hours, short Minutes, short Repetitions)
        {
            this.Name = Name;
            this.Order = Order;
            this.Hours = Hours;
            this.Minutes = Minutes;
            this.Repetitions = Repetitions;
        }

        [JsonPropertyName("name")]
        public string Name { get; }
        [JsonPropertyName("order")]
        public short Order { get; }
        [JsonPropertyName("hours")]
        public short Hours { get; }
        [JsonPropertyName("minutes")]
        public short Minutes { get; }
        [JsonPropertyName("repetitions")]
        public short Repetitions { get; }
    }
}
