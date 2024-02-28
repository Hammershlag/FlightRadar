using OOD_24L_01180689.src.factories;
using OOD_24L_01180689.src.readers;
using OOD_24L_01180689.src.writers;

class Program
{
    static void Main(string[] args)
    {
        string dir = Directory.GetCurrentDirectory() + "..\\..\\..\\..";
        string input = "data\\example1.ftr";
        string output = "data\\output.json";

        IFileReaderFactory fileFactory = new FTRReaderFactory();
        IDataSource dataSource = fileFactory.Create();

        var objects = dataSource.ReadData(dir, input);

        Console.WriteLine($"Deserialized data from file: {input}");

        IFileWriterFactory fileWriterFactory = new JSONWriterFactory();
        IWriter jsonWriter = fileWriterFactory.Create();

        jsonWriter.Write(objects, dir, output);

        Console.WriteLine($"Serialized data written to file: {output}");
    }
}