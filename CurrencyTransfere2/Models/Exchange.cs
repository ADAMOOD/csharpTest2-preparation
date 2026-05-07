using System.ComponentModel.DataAnnotations;

namespace CurrencyTransfere2.Models
{
    public class Exchange
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Email { get; set; }
        public double Ammount { get; set; }
        public string SelectedCurrency { get; set; }
        public double Result { get; set; }
    }
}
