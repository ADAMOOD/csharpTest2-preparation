using Dapper;

namespace Menza_XML.Models
{
    [Table("FoodRecord")]
    public class dbRecordModel
    {
        [Key]
        public int Id { get; set; }
        public string UserName { get; set; }
        public string OrderDate { get; set; }
        public int FoodAltId { get; set; }
        public string FoodName { get; set; }
    }
}
