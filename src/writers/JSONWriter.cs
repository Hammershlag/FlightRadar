using System.Text.Json;

namespace OOD_24L_01180689.src.writers
{
    public class JSONWriter : Writer
    {
        public override void Write(IEnumerable<object> objects, string dir, string filename)
        {
            var filePath = Path.Combine(dir, filename);

            if (!filePath.EndsWith(".json"))
            {
                throw new ArgumentException("Invalid file type");
            }

            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };
            string json = JsonSerializer.Serialize(objects, options);
            File.WriteAllText(filePath, json);
        }
    }
}