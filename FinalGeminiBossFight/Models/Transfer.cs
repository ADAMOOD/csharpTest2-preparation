
using Dapper;
namespace FinalGeminiBossFight.Models
{
    [Table("Transfer")]
    public class Transfer
    {
        [Key]
        public int Id { get; set; }
        [Dapper.Column("CURRENCYCODE")]
        public string CurrencyCode { get; set; }
        [Dapper.Column("NAME")]
        public string Name { get; set; }
        [Dapper.Column("EMAIL")]
        public string Email { get; set; }
        [Dapper.Column("AMMOUNT")]
        public double Ammount { get; set; }
        [Dapper.Column("ACCOUNT")]
        public string AccountNumber { get; set; }
        [Dapper.Column("COUNTY")]
        public string County { get; set; }
    }
}
