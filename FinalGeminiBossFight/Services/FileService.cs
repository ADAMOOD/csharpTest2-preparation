namespace FinalGeminiBossFight.Services
{
    public class FileService
    {
        public static async Task WriteObjectIntoFile(FileStream fs, object o)
        {
            StringWriter sw = new StringWriter();
            var type = o.GetType();
            await sw.WriteLineAsync($"Object => {type.Name}\n{{");
            foreach (var proppertyInfo in type.GetProperties())
            {
                if (proppertyInfo.PropertyType == typeof(string))
                {
                    sw.WriteLine($"#{proppertyInfo.Name} => {proppertyInfo.GetValue(o).ToString()};");
                }
            }
            await sw.WriteLineAsync("}");
            using (StreamWriter writer = new StreamWriter(fs))
            {
                await writer.WriteAsync(sw.ToString());
            }
        }
    }
}
