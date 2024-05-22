using NetworkSourceSimulator;
using OOD_24L_01180689.src.dataStorage;
using OOD_24L_01180689.src.logging;
using OOD_24L_01180689.src.observers;

namespace OOD_24L_01180689.src.updating;

public class EventUpdateManager : IObservable
{
    private static EventUpdateManager instance;
    private static readonly object lockObject = new();
    private readonly string input = "";
    private readonly int maxDelay = 1;
    private readonly int minDelay;

    private NetworkSourceSimulator.NetworkSourceSimulator networkSourceSimulator;
    private readonly List<IObserver> observers = new();

    private EventUpdateManager(string input, int minDelay, int maxDelay)
    {
        this.input = input;
        this.minDelay = minDelay;
        this.maxDelay = maxDelay;
    }

    public void AddObserver(IObserver observer)
    {
        observers.Add(observer);
    }

    public void RemoveObserver(IObserver observer)
    {
        observers.Remove(observer);
    }

    public static EventUpdateManager GetInstance(string input, int minDelay, int maxDelay, ILogger logger)
    {
        lock (lockObject)
        {
            if (instance == null)
            {
                instance = new EventUpdateManager(input, minDelay, maxDelay);
                instance.AddObserver(logger);

                instance.AddObserver(DataStorage.GetInstance);
            }
        }

        return instance;
    }

    public async Task Start()
    {
        networkSourceSimulator = new NetworkSourceSimulator.NetworkSourceSimulator(input, minDelay, maxDelay);
        networkSourceSimulator.OnIDUpdate += HandleIDUpdate;
        networkSourceSimulator.OnPositionUpdate += HandlePositionUpdate;
        networkSourceSimulator.OnContactInfoUpdate += HandleContactInfoUpdate;
        await StartNetworkSourceSimulatorAsync();
    }

    private async Task StartNetworkSourceSimulatorAsync()
    {
        await Task.Run(() => networkSourceSimulator.Run());
    }

    public Message GetMessageAt(int index)
    {
        return networkSourceSimulator.GetMessageAt(index);
    }

    private Task stopTask()
    {
        return Task.CompletedTask;
    }

    public async Task Stop()
    {
        try
        {
            await stopTask();
        }
        catch
        {
            Console.WriteLine("Server Interrupted");
        }
    }

    private void HandleIDUpdate(object sender, IDUpdateArgs e)
    {
        foreach (var obs in observers) obs.Update(e);
    }

    private void HandlePositionUpdate(object sender, PositionUpdateArgs e)
    {
        foreach (var obs in observers) obs.Update(e);
    }

    private void HandleContactInfoUpdate(object sender, ContactInfoUpdateArgs e)
    {
        foreach (var obs in observers) obs.Update(e);
    }
}