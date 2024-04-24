using System;
using System.Threading.Tasks;
using DynamicData;
using NetworkSourceSimulator;
using OOD_24L_01180689.src.dataStorage;
using OOD_24L_01180689.src.dto.entities;
using OOD_24L_01180689.src.dto.entities.cargo;
using OOD_24L_01180689.src.dto.entities.flights;
using OOD_24L_01180689.src.dto.entities.people;
using OOD_24L_01180689.src.logging;
using OOD_24L_01180689.src.observers;

namespace OOD_24L_01180689.src.updating
{
    public class EventUpdateManager : IObservable
    {
        private static EventUpdateManager instance;
        private static readonly object lockObject = new object();

        private NetworkSourceSimulator.NetworkSourceSimulator networkSourceSimulator;
        List<IObserver> observers = new List<IObserver>();
        private int minDelay = 0;
        private int maxDelay = 1;
        private string input = "";

        private EventUpdateManager(string input, int minDelay, int maxDelay)
        {
            this.input = input;
            this.minDelay = minDelay;
            this.maxDelay = maxDelay;
        }

        public static EventUpdateManager GetInstance(string input, int minDelay, int maxDelay, ILogger logger)
        {
            lock (lockObject)
            {
                if (instance == null)
                {
                    instance = new EventUpdateManager(input, minDelay, maxDelay);
                    instance.AddObserver(DataStorage.GetInstance);
                    instance.AddObserver(logger);
                }
            }

            return instance;
        }

        public async Task Start()
        {
            networkSourceSimulator = new NetworkSourceSimulator.NetworkSourceSimulator(input, minDelay, maxDelay);
            this.networkSourceSimulator.OnIDUpdate += HandleIDUpdate;
            this.networkSourceSimulator.OnPositionUpdate += HandlePositionUpdate;
            this.networkSourceSimulator.OnContactInfoUpdate += HandleContactInfoUpdate;
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
            foreach (var obs in observers)
            {
                obs.Update(e);
            }
        }

        private void HandlePositionUpdate(object sender, PositionUpdateArgs e)
        {
            foreach (var obs in observers)
            {
                obs.Update(e);
            }
        }

        private void HandleContactInfoUpdate(object sender, ContactInfoUpdateArgs e)
        {
            foreach (var obs in observers)
            {
                obs.Update(e);
            }
        }

        public void AddObserver(IObserver observer)
        {
            observers.Add(observer);
        }

        public void RemoveObserver(IObserver observer)
        {
            observers.Remove(observer);
        }
    }
}