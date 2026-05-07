using System.ComponentModel.DataAnnotations;

namespace Menza_XML.Models
{
    public class DateViewModel
    {
        [DataType(DataType.Date,ErrorMessage ="spatny format data")]
        public DateTime Date { get; set; } 
    }
}

