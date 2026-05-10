
using System.ComponentModel.DataAnnotations;

namespace FinalGeminiBossFight.Models
{

    public class FormViewModel
    {

        public int Id { get; set; }
        public string CurrencyCode { get; set; }
        [Required(ErrorMessage = "Jmeno je povinne pole")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Email je povinny")]
        [EmailAddress(ErrorMessage = "Nevalidni e-mail")]
        public string Email { get; set; }
        [Required(ErrorMessage = "castka je povinna")]
        [Range(0.01, double.MaxValue, ErrorMessage = $"castka musi byt aspon 0.01")]    
        public double Ammount { get; set; }
        [Required(ErrorMessage = "zadejte prosim ucet na ktery prevadite")]
        [RegularExpression(@"^[0-9]{1,6}-[0-9]{2,10}/[0-9]{5}$", ErrorMessage = "Neplatny format uctu napr. (4568-42494644/0300)")]
        public string AccountNumber { get; set; }


    }
}
