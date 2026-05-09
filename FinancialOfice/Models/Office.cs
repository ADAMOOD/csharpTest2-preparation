using System.Xml.Serialization;

namespace FinancialOfice.Models
{
    public class Office
    {
        [XmlElement("CHODNOTA")]
        public string Chodnota { get; set; }
        [XmlElement("TEXT")]
        public string Name { get; set; }

    }
}
