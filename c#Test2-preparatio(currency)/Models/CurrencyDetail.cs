using System.Text.Json.Serialization;

namespace c_Test2_preparatio_currency_.Models
{
    public class CurrencyDetail
    {
        [JsonPropertyName("dev_stred")]
        public double dev_stred { get; set; }
    }
}
