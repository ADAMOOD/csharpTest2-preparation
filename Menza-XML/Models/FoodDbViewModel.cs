using System.ComponentModel.DataAnnotations;

namespace Menza_XML.Models
{
    public class FoodDbViewModel
    {
        [Required(ErrorMessage = "Musite zadat jmeno")]
        public string Name { get; set; } = string.Empty;
        [Required(ErrorMessage = "Musíte mit vzbrane jidlo")]
        public FoodModel Food { get; set; }
        [Required(ErrorMessage = "Musite mit vybrane datum")]
        public string Date { get; set; }

    }
}
