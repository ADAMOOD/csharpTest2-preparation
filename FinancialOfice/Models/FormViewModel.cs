using System.ComponentModel.DataAnnotations;

namespace FinancialOfice.Models
{
   public  enum Gender
    {
        muz,
        zena,
        neurceno
    }
    public class FormViewModel
    {
        public string Chodnota { get; set; }
        [Required(ErrorMessage = "Jmeno je povinne")]
        public string Name { get; set; }
        [EmailAddress(ErrorMessage = "Nevalidni email")]
        [Required(ErrorMessage = "Email je povinny")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Pohlavi je povinne")]
        [EnumDataType(typeof(Gender))]
        public Gender Gender { get; set; }
        [Required(ErrorMessage = "zadejte prosim vasi mpoznaku")]
        [MaxLength(500,ErrorMessage = "prislis dlouha poznamka (Max. 500 znaku)")]
        public string Notes { get; set; }
    }
}
