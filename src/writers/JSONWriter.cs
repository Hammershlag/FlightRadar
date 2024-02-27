using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using OOD_24L_01180689.src.dto;
using OOD_24L_01180689.src.dto.airports;

namespace OOD_24L_01180689.src.writers
{
    public class JSONWriter : IWriter
    {
        public void Write(IEnumerable<object> objects, string filePath)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };
            string json = JsonSerializer.Serialize(objects, options);
            File.WriteAllText(filePath, json);
        }
    }

}
