using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace IC_WPF.Models
{
   public class Company
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Dic { get; set; }
        public string Town { get; set; }
        public string Notes { get; set; }
    }
}
