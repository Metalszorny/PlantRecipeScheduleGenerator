using System.Text.Json.Serialization;

namespace Common.Model
{
    public sealed class WateringPhase : Phase
    {
        public WateringPhase(string Name, short Order, short Hours, short Minutes, short Repetitions, short Amount)
            : base(Name, Order, Hours, Minutes, Repetitions)
        {
            this.Amount = Amount;
        }

        [JsonPropertyName("amount")]
        public short Amount { get; }
    }
}
