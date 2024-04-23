using OOD_24L_01180689.src.dataStorage;
using OOD_24L_01180689.src.factories.readers;
using OOD_24L_01180689.src.factories.writersFactories;
using OOD_24L_01180689.src.readers;
using OOD_24L_01180689.src.writers;

class ProgramPart1
{
    static void Main1(string[] args)
    {
        string dir = Directory.GetCurrentDirectory() + "..\\..\\..\\..";
        string input = "data\\input_example.ftr";
        string output = "data\\output.json";

        DataStorage ds = DataStorage.GetInstance;
        

        IFileReaderFactory fileFactory = new FTRReaderFactory();
        IDataSource dataSource = fileFactory.Create();

        dataSource.ReadData(Path.Combine(dir, input));

        Console.WriteLine($"Deserialized data from file: {input}");

        IFileWriterFactory fileWriterFactory = new JSONWriterFactory();
        IWriter jsonWriter = fileWriterFactory.Create();

        jsonWriter.Write(ds.GetObjectList(), dir, output);

        Console.WriteLine($"Serialized data written to file: {output}");
    }
}