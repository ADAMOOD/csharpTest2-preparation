using System.Text.Json.Serialization;

namespace CurrencyTransfere2.Models
{
    
    public class CurrencyModel
    {
        [JsonPropertyName("kurzy")]
        public Dictionary<string, CurrencyDetail> Currency { get; set; }//tady musi byt set pro serilizaci
    }
    public class CurrencyDetail
    {
        [JsonPropertyName("dev_stred")]
        public double dev_stred { get; set; }
    }
}
