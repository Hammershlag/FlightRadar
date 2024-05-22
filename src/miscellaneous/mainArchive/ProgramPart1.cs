using OOD_24L_01180689.src.dataStorage;
using OOD_24L_01180689.src.factories.readers;
using OOD_24L_01180689.src.factories.writersFactories;

internal class ProgramPart1
{
    private static void Main1(string[] args)
    {
        var dir = Directory.GetCurrentDirectory() + "..\\..\\..\\..";
        var input = "data\\input_example.ftr";
        var output = "data\\output.json";

        var ds = DataStorage.GetInstance;


        IFileReaderFactory fileFactory = new FTRReaderFactory();
        var dataSource = fileFactory.Create();

        dataSource.ReadData(Path.Combine(dir, input));

        Console.WriteLine($"Deserialized data from file: {input}");

        IFileWriterFactory fileWriterFactory = new JSONWriterFactory();
        var jsonWriter = fileWriterFactory.Create();

        jsonWriter.Write(ds.GetObjectList(), dir, output);

        Console.WriteLine($"Serialized data written to file: {output}");
    }
}