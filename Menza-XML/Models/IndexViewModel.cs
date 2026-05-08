using System.ComponentModel.DataAnnotations;

namespace Menza_XML.Models
{
    public class IndexViewModel
    {
        [DataType(DataType.Date,ErrorMessage ="spatny format data")]
        public DateTime Date { get; set; }

        public List<dbRecordModel> Orders { get; set; } = new List<dbRecordModel>();
    }
}

