using OOD_24L_01180689.src.factories;
using OOD_24L_01180689.src.readers;
using System.IO;
using OOD_24L_01180689.src.dto.airports;
using OOD_24L_01180689.src.writers;

class Program
{
    static void Main(string[] args)
    {
        FileReaderFactory fileFactory = new FTRReaderFactory();
        IDataSource dataSource = fileFactory.Create();
        var objects = dataSource.ReadData(Directory.GetCurrentDirectory() + "..\\..\\..\\..\\data\\example1.ftr");
        foreach (var obj in objects)
        {
            Console.WriteLine(obj.ToString());
        }
        Console.WriteLine();

        FileWriterFactory fileWriterFactory = new JSONWriterFactory();
        // Create a JSON writer
        IWriter jsonWriter = fileWriterFactory.Create();

        // Write objects to JSON file
        jsonWriter.Write(objects, Directory.GetCurrentDirectory() + "..\\..\\..\\..\\data\\output.json");

        Console.WriteLine("Serialized data written to file: output.json");
        Console.WriteLine(DateTime.Now.TimeOfDay);
    }
}
