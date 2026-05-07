using System.Text.Json.Serialization;

namespace c_Test2_preparatio_currency_.Models
{
    public class CurrencyResponse
    {
        public Dictionary<string, CurrencyDetail> Kurzy { get; set; }
    }
}
