namespace OOD_24L_01180689.src.logging;

public class ConsoleLogger : Logger
{
    public override void Log(string message)
    {
        try
        {
            Console.WriteLine($"{DateTime.Now}: {message}\n");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error while logging: {ex.Message}");
        }
    }
}