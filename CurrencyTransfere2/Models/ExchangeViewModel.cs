using System.ComponentModel.DataAnnotations;

namespace CurrencyTransfere2.Models
{
    public class ExchangeViewModel//view model ktery se pouziva ve View 
    {
        [Required(ErrorMessage = "Jméno je povinné")]
        public string Name { get; set; }

        [EmailAddress(ErrorMessage = "Neplatna E-mailova adresa")]
        public string? Email { get; set; }//pokud chceme at neni required musi byt nullable

        [Range(0.01, double.MaxValue, ErrorMessage = "Částka musí být kladná")]
        public double Ammount { get; set; }
        public string SelectedCurrency { get; set; }
        public CurrencyModel? Currency { get; set; } //zase nullable mohlo se nam vratit null z jsonu
        public double Result { get; set; }
    }
}
