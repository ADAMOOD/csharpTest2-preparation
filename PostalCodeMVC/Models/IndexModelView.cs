using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace PostalCodeMVC.Models
{
    public class IndexModelView
    {
        [MaxLength(5)]
        [RegularExpression(@"^[0-9]{5}$",ErrorMessage ="Musi to byt cislo")]
        public string PostalCode { get; set; } = string.Empty;
    }
}
