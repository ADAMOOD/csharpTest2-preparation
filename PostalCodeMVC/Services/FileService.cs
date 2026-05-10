namespace PostalCodeMVC.Services
{
    public class FileService
    {
        public static async Task WriteObjectToFilePostalCode(FileStream fs, object o)
        {
            StringWriter sw = new StringWriter();
            var type = o.GetType();
            await sw.WriteLineAsync($"Object => {type.Name}\n{{");
            foreach (var property in type.GetProperties())
            {

                sw.WriteLineAsync($"{property.Name} => {property.GetValue(o)}");


            }
            await sw.WriteLineAsync("}");
            using (StreamWriter streamWriter = new StreamWriter(fs))
            {
                streamWriter.Write(sw.ToString());
            }
        }
    }
}
