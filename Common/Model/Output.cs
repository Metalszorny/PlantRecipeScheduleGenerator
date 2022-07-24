using System.Text.Json.Serialization;

namespace Common.Model
{
    public abstract class Output
    {
        public Output()
        { }

        public Output(int trayNumber, DateTime startDate)
        {
            this.TrayNumber = trayNumber;
            this.StartDate = startDate;
        }

        [JsonPropertyName("trayNumber")]
        public int TrayNumber { get; set; }

        [JsonPropertyName("startDate")]
        public DateTime StartDate { get; set; }

        public override string ToString()
        {
            return $"{this.StartDate.ToString("yyyy-MM-ddTHH:mm:ss.fffffffZ")} {this.TrayNumber}";
        }
    }
}
