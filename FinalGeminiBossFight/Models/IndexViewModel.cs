using System.Text.Json.Serialization;

namespace FinalGeminiBossFight.Models
{
    public class IndexViewModel
    {
        [JsonPropertyName("kurzy")]
       public List<Currency> Currencies {  get; set; }
    }
    public class Currency
    {
        [JsonPropertyName("kod")]
        public string Code {  get; set; }
        [JsonPropertyName("kurz")]
        public double Course { get; set; }
    }
}
