using System.ComponentModel.DataAnnotations;

namespace PostalCodeMVC.Models
{
    public class FormViewModel
    {
        [Required(ErrorMessage ="Jmeno je povinne")]
        public string Name { get; set; }

        [Required(ErrorMessage ="Telefon je povinny")]
        [RegularExpression(@"^\+420[0-9]{9}$",ErrorMessage ="nevalidni telefonni cislo")]
        public string PhoneNumber { get; set; }

        [MaxLength(5)]
        [RegularExpression(@"^[0-9]{5}$", ErrorMessage = "Musi to byt cislo")]
        public string PostalCode { get; set; } = string.Empty;
        [Required(ErrorMessage ="Okres je povinny")]
        public string Country { get; set; }

    }
}
