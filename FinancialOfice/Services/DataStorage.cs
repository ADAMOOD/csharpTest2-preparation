using System.Reflection;
using System.Text;

namespace FinancialOfice.Services
{
    public class DataStorage
    {
        public static async Task<bool> SaveObjectToFile(string path, object o)
        {
            StringBuilder sb = new StringBuilder();

            var type = o.GetType();
            sb.AppendLine($"Object => {type.Name}");

            foreach (var propertyInfo in type.GetProperties())
            {
                if (propertyInfo.PropertyType == typeof(string))
                {
                    sb.AppendLine($"# {propertyInfo.Name} => {propertyInfo.GetValue(o).ToString()};");
                }
            }
            using (FileStream fs = new FileStream(path,FileMode.Create))//vytvori soubor pokud neexistuje
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    
                  await sw.WriteAsync(sb.ToString());
                }
            }
            return true;
        }
    }
}
