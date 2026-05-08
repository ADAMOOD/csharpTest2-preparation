using System.Xml.Serialization;

namespace Menza_XML.Models
{
    public class FoodModel
    {
        [XmlElement("name")]
        public string name { get; set; }
        [XmlElement("price")]
        public int price { get; set; }
        [XmlElement("mealKindId")]
        public int mealKindId { get; set; }
        [XmlElement("altId")]
        public int altId { get; set; }
    }
}
