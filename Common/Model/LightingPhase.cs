using System.Text.Json.Serialization;

namespace Common.Model
{
    public sealed class LightingPhase : Phase
    {
        public LightingPhase(string Name, short Order, short Hours, short Minutes, short Repetitions)
            : base(Name, Order, Hours, Minutes, Repetitions)
        { }

        ~LightingPhase()
        {
            if (this.Operations != null)
            {
                this.Operations.Clear();
            }
        }

        [JsonPropertyName("operations")]
        public ICollection<LightingPhaseOperation> Operations { get; set; }
    }
}
