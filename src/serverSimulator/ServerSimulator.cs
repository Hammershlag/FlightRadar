using NetworkSourceSimulator;

namespace OOD_24L_01180689.src.serverSimulator
{
    public class ServerSimulator
    {
        private static ServerSimulator instance;
        private static readonly object lockObject = new object();

        private int minDelay = 0;
        private int maxDelay = 1;
        private string input = "";
        private NetworkSourceSimulator.NetworkSourceSimulator networkSourceInstance;

        public event EventHandler<NewDataReadyArgs> OnDataReady;

        private ServerSimulator(string input, int minDelay, int maxDelay)
        {
            this.input = input;
            this.minDelay = minDelay;
            this.maxDelay = maxDelay;
        }

        public static ServerSimulator GetInstance(string input, int minDelay, int maxDelay)
        {
            lock (lockObject)
            {
                if (instance == null)
                {
                    instance = new ServerSimulator(input, minDelay, maxDelay);
                }
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
            Thread.Sleep(1000);
            //System.Environment.Exit(0);
            Console.WriteLine("Test");
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
}