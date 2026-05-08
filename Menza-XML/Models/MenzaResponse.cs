using System.Xml.Serialization;

namespace Menza_XML.Models
{
    [XmlRoot("MenzaResponse")]
    public class MenzaResponse
    {
        [XmlElement("item")]
        public List<FoodModel> Items { get; set; }
    }
}
