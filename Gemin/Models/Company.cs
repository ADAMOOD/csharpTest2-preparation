using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gemin.Models
{
    public enum CompanyTypes
    {
        sro,
        @as,
        osvc
    }
    [Table("Company")]
    public class Company
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Jmeno firmy je povinne")]
        public string Name { get; set; } = string.Empty;
        [Required(ErrorMessage = "DIC firmy je povinne")]
        public string DIC { get; set; }
        [Range(1, Int32.MaxValue, ErrorMessage = "Pocet zamestnancu misu byt cele cislo nejmene 1")]
        public int EmployeeCount { get; set; }
        [Required(ErrorMessage = "Pravni forma firmy je povinne")]
        [EnumDataType(typeof(CompanyTypes), ErrorMessage = "Neplatný datový typ")]
        public CompanyTypes CompanyType { get; set; }
        public string? Notes { get; set; }

        public static string getCompanyType(CompanyTypes type)
        {
            switch (type)
            {
                case CompanyTypes.@as:
                    {
                        return "a.s.";
                    }
                case CompanyTypes.osvc:
                    {
                        return "OSVC";
                    }
            }

            return "s.r.o";
        }
    }
}

