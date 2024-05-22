namespace OOD_24L_01180689.src.logging;

internal class FileLogger : Logger
{
    private readonly string filename = "";

    public FileLogger(string filename)
    {
        this.filename = filename;
    }

    public FileLogger()
    {
        var dir = Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..");
        var logDirectory = Path.Combine(dir, "data");
        if (!Directory.Exists(logDirectory)) Directory.CreateDirectory(logDirectory);

        var todayLogFileName = Path.Combine(logDirectory, $"log_{DateTime.Now.ToString("yyyy-MM-dd")}.txt");
        if (!File.Exists(todayLogFileName))
            using (var writer = File.CreateText(todayLogFileName))
            {
                writer.WriteLine($"Log created on {DateTime.Now}\n");
            }
        else
            using (var writer = File.AppendText(todayLogFileName))
            {
                writer.WriteLine($"\nLog updated on {DateTime.Now}\n");
            }


        filename = todayLogFileName;
    }

    public override void Log(string message)
    {
        try
        {
            using (var writer = File.AppendText(filename))
            {
                writer.WriteLine($"{DateTime.Now}: {message}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error while logging: {ex.Message}");
        }
    }
}