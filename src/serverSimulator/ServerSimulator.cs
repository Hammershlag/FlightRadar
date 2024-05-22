using NetworkSourceSimulator;

namespace OOD_24L_01180689.src.serverSimulator;

public class ServerSimulator
{
    private static ServerSimulator instance;
    private static readonly object lockObject = new();
    private readonly string input = "";
    private readonly int maxDelay = 1;

    private readonly int minDelay;
    private NetworkSourceSimulator.NetworkSourceSimulator networkSourceInstance;

    private ServerSimulator(string input, int minDelay, int maxDelay)
    {
        this.input = input;
        this.minDelay = minDelay;
        this.maxDelay = maxDelay;
    }

    public event EventHandler<NewDataReadyArgs> OnDataReady;

    public static ServerSimulator GetInstance(string input, int minDelay, int maxDelay)
    {
        lock (lockObject)
        {
            if (instance == null) instance = new ServerSimulator(input, minDelay, maxDelay);
        }

        return instance;
    }

    public void Start()
    {
        networkSourceInstance = new NetworkSourceSimulator.NetworkSourceSimulator(input, minDelay, maxDelay);
        networkSourceInstance.OnNewDataReady += (sender, e) => { OnDataReady?.Invoke(this, e); };
        StartNetworkSourceSimulator();
    }

    private void StartNetworkSourceSimulator()
    {
        Task.Run(networkSourceInstance.Run);
    }

    private Task stopTask()
    {
        return Task.CompletedTask;
    }

    public Message GetMessageAt(int index)
    {
        return networkSourceInstance.GetMessageAt(index);
    }

    public void Stop()
    {
        try
        {
            stopTask().Wait();
        }
        catch
        {
            Console.WriteLine("Server Interrupted");
        }
    }
}